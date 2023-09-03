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
	public class TeacherController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public TeacherController(ApplicationDbContext context)
		{
			_context = context;
		}


		[HttpGet]
		public async Task<ActionResult<Teacher>> GetTeacher(Guid id) 
		{
			var result = await _context.Teachers.Where(x => x.Id == id).ToListAsync();
			return Ok(result);
		}



		[HttpPost]
		public async Task<ActionResult<Teacher>> PostTeacher(TeacherViewModel teacherRequest)
		{
			if (_context.Teachers.Any(d => d.TeacherName == teacherRequest.TeacherName || d.TeacherEmail == teacherRequest.TeacherEmail))
			{
				return BadRequest("Course code or Course name already exists");
			}

			var teacher = new Teacher();
			teacher.Id = Guid.NewGuid();
			teacher.TeacherName = teacherRequest.TeacherName;
			teacher.TeacherAddress = teacherRequest.TeacherAddress;	
			teacher.TeacherEmail = teacherRequest.TeacherEmail;
			teacher.ContactNo = teacherRequest.ContactNo;
			teacher.Designation = teacherRequest.Designation;
			//teacher.Department = teacherRequest.Department;
			teacher.CreditToBeTaken = teacherRequest.CreditToBeTaken;
			teacher.DepartmentId = teacherRequest.DepartmentId;	

			_context.Teachers.Add(teacher);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacherRequest);



		}




	}



}
