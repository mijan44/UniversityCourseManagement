using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniversityCourseManagement.Data;
using UniversityCourseManagement.Helpers;
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
		public async Task<ActionResult<Course>>GetCourses()
		{
			
			var result = from t in _context.Courses
						 join d in _context.Departments on t.DepartmentID equals d.Id
						 select new
						 {
							 t.CourseName,
							 t.CourseCode,
							 t.CourseCredit,
							 t.CourseDescription,
							 t.Semester,
							 t.IsDeleted,
							
							 DepartmentId = d.Id,
							 DepartmentName = d.Name,
							 t.Id,
							

						 };
			return Ok(result.Where(x=>!x.IsDeleted));
			
		}



		[HttpPost]
		public async Task<ActionResult<Course>> PostCourse(Course courseRequest)
		{
			var existCourse = _context.Courses.FirstOrDefault(x=>x.CourseName == courseRequest.CourseName || x.CourseCode == courseRequest.CourseCode);
			if (existCourse == null)
			{
				if (courseRequest.Id == null || courseRequest.Id == new Guid("00000000-0000-0000-0000-000000000000"))
				{

					Validation validation = new Validation();
					if(validation.CheckSpecialChar(courseRequest.CourseCode))
					return Ok(new { ststusCode = 200, message = courseRequest.CourseCode + " - Special Character Not Allowed" });


					var course = new Course();
					course.Id = Guid.NewGuid();
					course.CourseCode = courseRequest.CourseCode;
					course.CourseName = courseRequest.CourseName;
					course.CourseCredit = courseRequest.CourseCredit;
					course.CourseDescription = courseRequest.CourseDescription;
					course.Semester = courseRequest.Semester;
					course.DepartmentID = courseRequest.DepartmentID;




					_context.Courses.Add(course);

					await _context.SaveChangesAsync();
					return Ok(new { ststusCode = 200, message = course.CourseName + " Course Saved SuccessFully" });

				}
				
			}
			else
			{
				var existingCourse = _context.Courses.FirstOrDefault(d => d.Id == courseRequest.Id);
				{
				
					existingCourse.CourseCode = courseRequest.CourseCode;
					existingCourse.CourseName = courseRequest.CourseName;
					existingCourse.CourseCredit = courseRequest.CourseCredit;
					existingCourse.CourseDescription = courseRequest.CourseDescription;

					_context.SaveChanges();
					return Ok(new { ststusCode = 204, message = existingCourse.CourseName + " Course Updated SuccessFully" });

				}

			}




			return Ok("Already Exist Course");

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
				return NotFound(new { ststusCode = 204, message =  " Course Deleted Failed" });
			}
			course.IsDeleted = true;
			_context.Courses.Update(course);

			await _context.SaveChangesAsync();

			return Ok(new { ststusCode = 200, message = course.CourseName + " Course Deleted SuccessFully" });
		}



		[HttpDelete]
		public async Task<IActionResult> DeleteAllCourses()
		{

			var courses = await _context.Courses.ToListAsync();
			
			foreach(var course in courses)
			{
				course.IsDeleted = true;
			}

			await _context.SaveChangesAsync();

			return Ok(new { ststusCode = 200, message =  " Course Unassigned SuccessFully" });
		}






	}
}
