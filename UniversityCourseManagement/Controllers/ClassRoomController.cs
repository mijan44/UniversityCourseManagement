using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using UniversityCourseManagement.Data;
using UniversityCourseManagement.Models;
using UniversityCourseManagement.ViewModel;

namespace UniversityCourseManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClassRoomController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public ClassRoomController (ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Route("GetViewClassSchedule")]
		public async Task<ActionResult<ViewClassSchedule>> GetViewClassSchedule(string DepartmentId)
		{
			List<ViewClassSchedule> list = new List<ViewClassSchedule>();
			var query = (from d in _context.Departments
						join cr in _context.ClassRooms on d.Id equals cr.DepartmentId
						join c in _context.Courses on cr.CourseId equals c.Id

						select new
						{
							CourseCode = c.CourseCode,
							CourseName = c.CourseName,
							DepartmentId = d.Id,
							cr.From,
							cr.To,
							ScheduleInfo = "R. No :"+ cr.RoomNo + ","+ cr.Day.Substring(0,3)+","+ string.Format("{0:hh:mm tt}", cr.From) + " - "+ string.Format("{0:hh:mm tt}", cr.To) + ";"

						}).Where(x=>x.DepartmentId==new Guid(DepartmentId)).ToList();

			if (query.Any())
			{
				var groupByCourse = from std in query
									group std by std.CourseCode into stdgroup 
									orderby stdgroup.Key
									select new
									{
										key = stdgroup.Key,
										student = stdgroup.OrderBy(x=>x.CourseName)

									};

				foreach (var group in groupByCourse)
				{
					ViewClassSchedule obj = new ViewClassSchedule();
					obj.CourseCode = group.key;
					obj.CourseName = group.student.Select(x=>x.CourseName).FirstOrDefault();
					string schedule = " ";
					var firstItem = group.student.First();

					foreach (var stu in group.student) 
					{ 
						if (stu.Equals(firstItem)){

							schedule = stu.ScheduleInfo.ToString();
						}
						else
						{
							schedule = schedule + " " + stu.ScheduleInfo.ToString();
						}

					}
					obj.ScheduleInfo = schedule;
					list.Add(obj);
				}

			}

			return Ok(list);

		}

		[HttpGet]
		[Route("GetCourseByDepartmentId")]
		public async Task<ActionResult<Course>> GetCourseByDepartmentId(string DepartmentId)
		{
			var result = await _context.Courses.Where (x=>x.DepartmentID == new Guid (DepartmentId)).ToListAsync();
			return Ok(result);
		}

		[HttpGet]
		public async Task<ActionResult<ClassRoomViewModel>> GetAllClass()
		{
			List <ClassRoomViewModel> classRoomList = new List <ClassRoomViewModel> ();
			var result = await _context.ClassRooms.ToListAsync();
			if (result.Count > 0)
			{
				for (int i = 0; i < result.Count; i++)
				{
					ClassRoomViewModel obj =new ClassRoomViewModel ();
					obj.Id = result[i].Id;
					obj.CourseId = result[i].CourseId;
					obj.DepartmentId = result[i].DepartmentId;
					obj.RoomNo = result[i].RoomNo;
					obj.DepartmentName = _context.Departments.Where(x => x.Id == result[i].DepartmentId).Select(x=>x.Name).FirstOrDefault();
					obj.CourseName = _context.Courses.Where(x => x.Id == result[i].CourseId).Select(x => x.CourseName).FirstOrDefault();
					obj.From= result[i].From.ToString("HH:mm");
					obj.To = result[i].To.ToString("HH:mm");
					obj.Day = result[i].Day;
					classRoomList.Add (obj);
				}
			}
			return Ok(classRoomList);
		}


		[HttpPost]
		public async Task<ActionResult<ClassRoom>>PostClassRoom (ClassRoomViewModel requestClassRoom)
		{
			DateTime fromDate = Convert.ToDateTime(requestClassRoom.From);
			DateTime toDate = Convert.ToDateTime(requestClassRoom.To);
			if (requestClassRoom.Id == null || requestClassRoom.Id == new Guid("00000000-0000-0000-0000-000000000000"))
			{
				var existRoom = _context.ClassRooms.Where(x => x.RoomNo == requestClassRoom.RoomNo && x.Day == requestClassRoom.Day && (x.From >= fromDate || x.To<= toDate)).FirstOrDefault();
				if (existRoom == null)
				{ 
				var classRoom = new ClassRoom();
				classRoom.Id = requestClassRoom.Id;
				classRoom.RoomNo = requestClassRoom.RoomNo;
				classRoom.Day = requestClassRoom.Day;
				classRoom.From = fromDate;
				classRoom.To = toDate;
				classRoom.CourseId = requestClassRoom.CourseId;
				classRoom.DepartmentId = requestClassRoom.DepartmentId;

					_context.ClassRooms.Add(classRoom);
					await _context.SaveChangesAsync();


				}
				else
				{
					return Ok("Already Exist Time");
				}


				
				
				

			}
			else
			{
				var existingClassRoom = _context.ClassRooms.FirstOrDefault(x=>x.Id == requestClassRoom.Id);
				if (existingClassRoom == null)
				{
					return  NotFound();

					existingClassRoom.RoomNo = requestClassRoom.RoomNo;
					existingClassRoom.Day = requestClassRoom.Day;
					existingClassRoom.From = fromDate;
					existingClassRoom.To = toDate;

					_context.SaveChanges();

				}

			}


			return Ok();

			
		}

		private bool RoomAvailable(int id)
		{
			return _context.ClassRooms.Any(id => _context.ClassRooms.Any());
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteClassRoom(Guid id)
		{

			var classRoom = await _context.ClassRooms.Where(x => x.Id == id).FirstAsync();
			if (classRoom == null)
			{
				return NotFound();
			}
			_context.ClassRooms.Remove(classRoom);

			await _context.SaveChangesAsync();

			return Ok();
		}



		[HttpDelete]
		public async Task<IActionResult> DeleteAllClassRoom()
		{

			var classRoom = await _context.ClassRooms.FirstAsync();
			
			_context.ClassRooms.Remove(classRoom);

			await _context.SaveChangesAsync();

			return Ok();
		}










	}
}
