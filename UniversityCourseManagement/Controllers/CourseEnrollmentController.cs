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
	public class CourseEnrollmentController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public CourseEnrollmentController (ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<CourseEnrollment>> GetCourseEnrollment(Guid id)
		{
			var result = await _context.CourseEnrollments.Where(x => x.Id == id).ToListAsync();
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<CourseEnrollment>> PostCourseEnrollment(CourseEnrollmentViewModel requestCourseEnrollment)
		{
			if (_context.CourseEnrollments.Any(d => d.Id == requestCourseEnrollment.Id || d.RegistrationNumber == requestCourseEnrollment.RegistrationNumber))
			{
				return BadRequest("Course exisit");
			}

		

			var courseEnroll = new CourseEnrollment();
			courseEnroll.Id = requestCourseEnrollment.Id;
			courseEnroll.RegistrationNumber = requestCourseEnrollment.RegistrationNumber;
			courseEnroll.EnrollmentDate = requestCourseEnrollment.EnrollmentDate;
			courseEnroll.CourseId = requestCourseEnrollment.CourseId;



			_context.CourseEnrollments.Add(courseEnroll);
			await _context.SaveChangesAsync();
			return CreatedAtAction("GetCourseEnrollment", new { id = courseEnroll.Id }, requestCourseEnrollment);
		}


		[HttpPut]
		public IActionResult UpdateCourseEnrollment(Guid id, CourseEnrollment updateCourseEnrollment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var existingCourseEnrollment = _context.CourseEnrollments.FirstOrDefault(d => d.Id == id);
			{
				if (existingCourseEnrollment == null)
				{
					return NotFound();
				}

				existingCourseEnrollment.RegistrationNumber = updateCourseEnrollment.RegistrationNumber;
				existingCourseEnrollment.EnrollmentDate = updateCourseEnrollment.EnrollmentDate;
				existingCourseEnrollment.CourseId = updateCourseEnrollment.CourseId;
		

				_context.SaveChanges();
				return Ok();
			}

			//bool CourseAvailable(int id)
			//{
			//	return true;
			//}


		}

		private bool EnrollCourseAvailable(int id)
		{
			return _context.CourseEnrollments.Any(id => _context.CourseEnrollments.Any());
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCourse(Guid id)
		{

			var courseEnrollment = await _context.CourseEnrollments.Where(x => x.Id == id).FirstAsync();
			if (courseEnrollment == null)
			{
				return NotFound();
			}
			_context.CourseEnrollments.Remove(courseEnrollment);

			await _context.SaveChangesAsync();

			return Ok();
		}

	}
}
