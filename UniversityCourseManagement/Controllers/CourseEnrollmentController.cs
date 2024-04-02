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

		[HttpGet]
		[Route("GetAllCourseEnroll")]
		public async Task<ActionResult<CourseEnrollmentViewModel>> GetAllCourseEnrollment()
		{
			var result = await (from ce in _context.CourseEnrollments
								join s in _context.RegisterStudents on ce.StudentId equals s.Id
								join d in _context.Departments on s.DepartmentId equals d.Id
								join c in _context.Courses on ce.CourseId equals c.Id
								select new
								{
									ce.Id,
									ce.StudentId,
									StudentName = s.StudentName,
									StudentEmail = s.StudentEmail,
									DepartmentId = s.DepartmentId,
									DepartmentName = d.Name,
									ce.CourseId,
									ce.IsDeleted,
									RegistrationNumber = s.RegistrationNumber,
									CourseName = c.CourseName,
									ce.EnrollmentDate
								}).ToListAsync();

			return Ok(result.Where(x=>!x.IsDeleted));
		}


		[HttpPost]
		public async Task<ActionResult<CourseEnrollment>> PostCourseEnrollment(CourseEnrollment requestCourseEnrollment)
		{
			var existEnrollCourse = _context.CourseEnrollments.FirstOrDefault(x=>x.StudentId == requestCourseEnrollment.StudentId && x.CourseId == requestCourseEnrollment.CourseId && !x.IsDeleted );
			if (existEnrollCourse == null)
			{
				if (requestCourseEnrollment.Id == null || requestCourseEnrollment.Id == new Guid("00000000-0000-0000-0000-000000000000"))
				{

					var courseEnroll = new CourseEnrollment();
					courseEnroll.Id = Guid.NewGuid();

					courseEnroll.EnrollmentDate = requestCourseEnrollment.EnrollmentDate;
					courseEnroll.CourseId = requestCourseEnrollment.CourseId;
					courseEnroll.StudentId = requestCourseEnrollment.StudentId;



					_context.CourseEnrollments.Add(courseEnroll);
					await _context.SaveChangesAsync();
					return Ok(new { ststusCode = 200, message = " Course Enrolled SuccessFully" });

				}
				

			}
			else
			{
				var existingCourseEnrollment = _context.CourseEnrollments.FirstOrDefault(d => d.Id == requestCourseEnrollment.Id);
				{


					existingCourseEnrollment.EnrollmentDate = requestCourseEnrollment.EnrollmentDate;
					existingCourseEnrollment.CourseId = requestCourseEnrollment.CourseId;
					existingCourseEnrollment.EnrollmentDate = requestCourseEnrollment.EnrollmentDate;



					_context.SaveChanges();
					return Ok(new { ststusCode = 200, message = " Course Enroll Updated SuccessFully" });

				}


			}

			return Ok("Course Enrollment Exist");




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
				return NotFound(new { ststusCode = 204, message = " Course Enrollment Deleted Failed" });
			}
			courseEnrollment.IsDeleted = true;
			_context.CourseEnrollments.Update(courseEnrollment);

			await _context.SaveChangesAsync();

			return Ok(new { ststusCode = 204, message = " Course Enrollment Deleted Successfully" });
		}

	}
}
