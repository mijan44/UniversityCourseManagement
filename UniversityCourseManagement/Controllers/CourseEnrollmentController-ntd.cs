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
			courseEnroll.DepartmentId = requestCourseEnrollment.DepartmentId;
			courseEnroll.CourseId = requestCourseEnrollment.CourseId;







			_context.CourseEnrollments.Add(courseEnroll);
			await _context.SaveChangesAsync();
			return CreatedAtAction("GetCourseEnrollment", new { id = courseEnroll.Id }, requestCourseEnrollment);
		}
	}
}
