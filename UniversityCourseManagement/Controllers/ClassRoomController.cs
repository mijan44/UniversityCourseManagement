using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		//[HttpGet]
		//public async Task<ActionResult<ClassRoom>> GetClassRoom (Guid id)
		//{
		//	var result = await _context.ClassRooms.Where(x => x.Id == id).ToListAsync();
		//	return Ok(result);
		//}



		[HttpGet]
		public async Task<ActionResult<ClassRoom>> GetAllClass()
		{
			var result = await _context.ClassRooms.ToListAsync();
			return Ok(result);
		}


		[HttpPost]
		public async Task<ActionResult<ClassRoom>>PostClassRoom (ClassRoomViewModel requestClassRoom)
		{
			if(_context.ClassRooms.Any(d => d.Id == requestClassRoom.Id || d.RoomNo == requestClassRoom.RoomNo)) 
			{
				return BadRequest("Class rooms are exisit");
			}

			string FromTime = requestClassRoom.FromTime;
			string FromAmPm = requestClassRoom.FromAmPm;
			string ToTime = requestClassRoom.ToTime;
			string ToAmPm = requestClassRoom.ToAmPm;
			

			var classRoom = new ClassRoom();
			classRoom.Id = requestClassRoom.Id;
			classRoom.RoomNo = requestClassRoom.RoomNo;
			classRoom.Day = requestClassRoom.Day;
			classRoom.From = Convert.ToDateTime(FromTime + FromAmPm);
			classRoom.To = Convert.ToDateTime(ToAmPm + ToTime);
			classRoom.CourseId = requestClassRoom.CourseId;
			classRoom.DepartmentId = requestClassRoom.DepartmentId;
			


			
			_context.ClassRooms.Add(classRoom);
			await _context.SaveChangesAsync();
			return CreatedAtAction("GetClassRoom", new { id = classRoom.Id }, requestClassRoom);
		}


	}
}
