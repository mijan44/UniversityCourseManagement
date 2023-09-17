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
	public class CourseAssignmentTeacherController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public CourseAssignmentTeacherController (ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<CourseAssignmentTeacher>> GetAssignTeacher(Guid id) 
		{
			var result = await _context.CourseAssignmentTeachers.Where(x => x.Id == id).ToListAsync();
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<CourseAssignmentTeacher>> PostAssignTeacher(AssignTeacherViewModel assignCourseTeacherRequest)
		{
			if(_context.CourseAssignmentTeachers.Any(d => d.TeacherId == assignCourseTeacherRequest.TeacherId || d.CourseId == assignCourseTeacherRequest.CourseId))
			{
				return BadRequest("Teacher already exist");
			}
			var assignTeacher = new CourseAssignmentTeacher();
			assignTeacher.Id = Guid.NewGuid();
			assignTeacher.TeacherId = assignCourseTeacherRequest.TeacherId;
			assignTeacher.CourseId = assignCourseTeacherRequest.CourseId;
			assignTeacher.AssignedCredit = assignCourseTeacherRequest.AssignedCredit;
			assignTeacher.RemainingCredit = assignCourseTeacherRequest.RemainingCredit;
			assignTeacher.Department = assignCourseTeacherRequest.Department;


			_context.CourseAssignmentTeachers.Add(assignTeacher);
			await _context.SaveChangesAsync();
			return CreatedAtAction("GetAssignTeacher", new { id = assignTeacher.Id }, assignCourseTeacherRequest);


		}
		[HttpPut]
		public IActionResult UpdateCourseAssignTeacher(Guid id, CourseAssignmentTeacher updateCourseAssignTeacher)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var existingCourseAssignTeacher = _context.CourseAssignmentTeachers.FirstOrDefault(d => d.Id == id);
			{
				if (existingCourseAssignTeacher == null)
				{
					return NotFound();
				}
				existingCourseAssignTeacher.AssignedCredit += updateCourseAssignTeacher.AssignedCredit;
				existingCourseAssignTeacher.RemainingCredit += updateCourseAssignTeacher.RemainingCredit;
				existingCourseAssignTeacher.Department = updateCourseAssignTeacher.Department;



				_context.SaveChanges();
				return Ok();
			}


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
				return NotFound();
			}
			_context.CourseAssignmentTeachers.Remove(courseAssignteacher);

			await _context.SaveChangesAsync();

			return Ok();
		}









	}

}
