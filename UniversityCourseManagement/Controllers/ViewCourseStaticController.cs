using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityCourseManagement.Data;
using UniversityCourseManagement.ViewModel;

namespace UniversityCourseManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ViewCourseStaticController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public ViewCourseStaticController(ApplicationDbContext context)
		{
			_context = context;
		}



		[HttpGet]
		public async Task<ActionResult<ViewCourseStatic>> GetStaticCourse(string DepartmentId)
		{
			var query = (from cat in _context.CourseAssignmentTeachers
						join c in _context.Courses on cat.CourseId equals c.Id
						join d in _context.Departments on cat.DepartmentId equals d.Id
						join t in _context.Teachers on cat.TeacherId equals t.Id
						select new
						{
							CourseCode = c.CourseCode,
							CourseName = c.CourseName,
							Semester = c.Semester,
							TeacherName = t.TeacherName,
							DepartmentId = d.Id
						}).Where(x=>x.DepartmentId==new Guid(DepartmentId));
			return Ok(query);

		}
		

	}
}
