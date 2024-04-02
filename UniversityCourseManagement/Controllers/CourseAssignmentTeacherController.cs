using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UniversityCourseManagement.Data;
using UniversityCourseManagement.Models;
using UniversityCourseManagement.ViewModel;

namespace UniversityCourseManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CourseAssignmentTeacherController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public CourseAssignmentTeacherController (ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Route("GetAssignedCourse")]
		public async Task<ActionResult<AssignTeacherViewModel>> GetAssignedCourse()
		{
			List<AssignTeacherViewModel> lst = new List<AssignTeacherViewModel>();

			var query = from cat in _context.CourseAssignmentTeachers
						join t in _context.Teachers on cat.TeacherId equals t.Id
						join c in _context.Courses on cat.CourseId equals c.Id
						join d in _context.Departments on cat.DepartmentId equals d.Id
						select new
						{
							Id = cat.Id,
							TeacherId = cat.TeacherId,
							CourseId = cat.CourseId,
							DepartmentId = cat.DepartmentId,
							TeacherName = t.TeacherName,
							CourseName = c.CourseName,
							DepartmentName = d.Name,
							AssignedCredit = t.CreditToBeTaken,
							RemainingCredit = cat.RemainingCredit,
							CourseCredit = c.CourseCredit,
							cat.IsDeleted

						};

			

			foreach (var item in query)
			{
				AssignTeacherViewModel obj = new AssignTeacherViewModel();

				obj.Id = item.Id;
				obj.AssignedCredit = item.AssignedCredit;
				obj.RemainingCredit = item.RemainingCredit;
				obj.CourseId = item.CourseId;
				obj.DepartmentId = item.DepartmentId;
				obj.TeacherId = item.TeacherId;
				obj.CourseName = item.CourseName;
				obj.DepartmentName = item.DepartmentName;
				obj.TeacherName = item.TeacherName;
				obj.CourseCredit = Convert.ToDecimal (item.CourseCredit);
				obj.IsDeleted = item.IsDeleted;
				
				lst.Add(obj);
			}


			return Ok(lst.Where(x=>!x.IsDeleted));

		}



		[HttpGet]  /// to get remain credit 
		[Route("GetRemainingCredit")]
		public decimal GetRemainingCredit(string teacherId)
		{
			var creditTobeTaken = _context.Teachers.Where(x => x.Id == new Guid(teacherId)).Select(x=>x.CreditToBeTaken).FirstOrDefault();
			var creditTaken = _context.CourseAssignmentTeachers.Where(x => x.TeacherId == new Guid(teacherId)).Sum(x=>x.AssignedCredit);

			var remainCredit = Convert.ToDecimal(creditTobeTaken) - creditTaken;
			return remainCredit;
		}

		[HttpGet]
		public async Task<ActionResult<CourseAssignmentTeacher>> GetAssignTeacher() 
		{
			var result = await _context.CourseAssignmentTeachers.ToListAsync();
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<CourseAssignmentTeacher>> PostAssignTeacher(CourseAssignmentTeacher assignCourseTeacherRequest)
		{
			var existAssignTeacher = _context.CourseAssignmentTeachers.FirstOrDefault(x=>x.CourseId == assignCourseTeacherRequest.CourseId && x.TeacherId==assignCourseTeacherRequest.TeacherId && !x.IsDeleted);
			if (existAssignTeacher == null)
			{
				if (assignCourseTeacherRequest.Id == null || assignCourseTeacherRequest.Id == new Guid("00000000-0000-0000-0000-000000000000"))
				{


					var assignTeacher = new CourseAssignmentTeacher();
					assignTeacher.Id = Guid.NewGuid();
					assignTeacher.TeacherId = assignCourseTeacherRequest.TeacherId;
					assignTeacher.CourseId = assignCourseTeacherRequest.CourseId;
					assignTeacher.AssignedCredit = Convert.ToDecimal(_context.Courses.Where(x => x.Id == assignCourseTeacherRequest.CourseId).Select(x => x.CourseCredit).FirstOrDefault());

					assignTeacher.RemainingCredit = Convert.ToDecimal(GetRemainingCredit(assignCourseTeacherRequest.TeacherId.ToString()));
					assignTeacher.DepartmentId = assignCourseTeacherRequest.DepartmentId;


					_context.CourseAssignmentTeachers.Add(assignTeacher);
					await _context.SaveChangesAsync();
					return Ok(new { ststusCode = 200, message = " Teacher Assigned SuccessFully" });

				}

			}
			else
			{
				var existingCourseAssignTeacher = _context.CourseAssignmentTeachers.FirstOrDefault(d => d.Id == assignCourseTeacherRequest.Id);
				{
					
					existingCourseAssignTeacher.AssignedCredit = Convert.ToDecimal(_context.Courses.Where(x => x.Id == assignCourseTeacherRequest.CourseId).Select(x => x.CourseCredit).FirstOrDefault());
					existingCourseAssignTeacher.RemainingCredit = Convert.ToDecimal(GetRemainingCredit(assignCourseTeacherRequest.TeacherId.ToString()));
					existingCourseAssignTeacher.CourseId = assignCourseTeacherRequest.CourseId;


					_context.SaveChanges();
					return Ok(new { ststusCode = 204, message = " Teacher Assigned Updated SuccessFully" });

				}


			}



			return Ok("Already Teacher Assigned Exist");

		}
		





		private bool CourseAssignTeacherAvailable(int id)
		{
			return _context.CourseAssignmentTeachers.Any(id => _context.CourseAssignmentTeachers.Any());
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCourseAssignTeacher(Guid id)
		{

			var courseAssignteacher = await _context.CourseAssignmentTeachers.Where(x => x.Id == id).FirstAsync();
			if (courseAssignteacher == null)
			{
				return NotFound(new { ststusCode = 204, message = " Teacher Assigned Deleted Failed" });
			}
			courseAssignteacher.IsDeleted = true;
			_context.CourseAssignmentTeachers.Update(courseAssignteacher);

			await _context.SaveChangesAsync();

			return Ok(new { ststusCode = 200, message = " Teacher Assigned Deleted SuccessFully" });
		}









	}

}
