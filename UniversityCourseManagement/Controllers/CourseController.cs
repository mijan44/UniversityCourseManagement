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
	public class CourseController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public CourseController (ApplicationDbContext context)
		{
			_context = context;
		}


		[HttpGet]
		public async Task<ActionResult<Course>>GetCourses(Guid id)
		{
			var result= await _context.Courses.Where(x => x.Id == id).ToListAsync();
			return Ok(result);
		}


		[HttpPost]
		public async Task<ActionResult<Course>> PostCourse(CourseViewModel courseRequest)
		{
			if(_context.Courses.Any(d => d.CourseCode == courseRequest.CourseCode || d.CourseName == courseRequest.CourseName )) 
			{
				return BadRequest("Course code or Course name already exists");
			}

			var course = new Course();
			course.Id = Guid.NewGuid();
			course.CourseCode = courseRequest.CourseCode;
			course.CourseName = courseRequest.CourseName;
			course.CourseCredit = courseRequest.CourseCredit;
			course.CourseDescription = courseRequest.CourseDescription;

			_context.Courses.Add(course);
			
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCourses", new { id = course.Id }, courseRequest);

		}






	}
}
