using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
			//course.Semester = courseRequest.Semester;
			//course.Department = courseRequest.Department;
			course.DepartmentID = courseRequest.DepartmentID;
			course.SemesterID = courseRequest.SemesterID;



			_context.Courses.Add(course);
			
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCourses", new { id = course.Id }, courseRequest);

		}

		[HttpPut]
		public IActionResult UpdateCourse (Guid id, Course updateCourse)
		{
			if (!ModelState.IsValid)	
			{
				return BadRequest(ModelState);
			}
			var existingCourse = _context.Courses.FirstOrDefault(d => d.Id == id);
			{
				if (existingCourse == null)
				{
					return NotFound();
				}

				existingCourse.CourseCode = updateCourse.CourseCode;
				existingCourse.CourseName = updateCourse.CourseName;
				existingCourse.CourseCredit = updateCourse.CourseCredit;
				existingCourse.CourseDescription = updateCourse.CourseDescription;

				_context.SaveChanges();
				return Ok();
			}

			//bool CourseAvailable(int id)
			//{
			//	return true;
			//}


		}



		private bool CourseAvailable(int id)
		{
			return _context.Courses.Any(id => _context.Courses.Any());
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCourse(Guid id)
		{

			var course = await _context.Courses.Where(x => x.Id == id).FirstAsync();
			if (course == null)
			{
				return NotFound();
			}
			_context.Courses.Remove(course);

			await _context.SaveChangesAsync();

			return Ok();
		}






	}
}
