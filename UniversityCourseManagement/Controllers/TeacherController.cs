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

		//update teacher
		[HttpPut]
		public IActionResult UpdateTeacher(Guid id, Teacher updateTeacher)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var existingTeacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
			if (existingTeacher == null)
			{
				return NotFound();
			}

			existingTeacher.TeacherName = updateTeacher.TeacherName;
			existingTeacher.TeacherAddress = updateTeacher.TeacherAddress;
			existingTeacher.TeacherEmail = updateTeacher.TeacherEmail;
			existingTeacher.ContactNo = updateTeacher.ContactNo;
			existingTeacher.Designation = updateTeacher.Designation;
			existingTeacher.CreditToBeTaken = updateTeacher.CreditToBeTaken;
			
			_context.SaveChanges();
			return Ok();

		}

		private bool TeacherAvailable(int id)
		{
			return _context.Teachers.Any(id => _context.Teachers.Any());
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTeacher(Guid id)
		{

			var teacher = await _context.Teachers.Where(x => x.Id == id).FirstAsync();
			if (teacher == null)
			{
				return NotFound();
			}
			_context.Teachers.Remove(teacher);

			await _context.SaveChangesAsync();

			return Ok();
		}



	}



}
