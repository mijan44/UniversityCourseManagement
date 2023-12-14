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
		[Route("GetTeacherByDepartment")]
		public async Task<ActionResult<Teacher>> GetTeacherByDepartment(Guid DepartmentId)
		{
			var result = await _context.Teachers.Where(x => x.DepartmentId == DepartmentId).ToListAsync();
			return Ok(result);
		}



		[HttpGet]
		public async Task<ActionResult<Teacher>> GetTeacher() 
		{
			var result = await _context.Teachers.ToListAsync();
			return Ok(result);
		}



		[HttpPost]
		public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacherRequest)
		{
			if (teacherRequest.Id == null || teacherRequest.Id == new Guid("00000000-0000-0000-0000-000000000000"))
			{
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

			}
			else
			{
				var existingTeacher = _context.Teachers.FirstOrDefault(t => t.Id == teacherRequest.Id);
				if (existingTeacher == null)
				{
					return NotFound();
				}

				existingTeacher.TeacherName = teacherRequest.TeacherName;
				existingTeacher.TeacherAddress = teacherRequest.TeacherAddress;
				existingTeacher.TeacherEmail = teacherRequest.TeacherEmail;
				existingTeacher.ContactNo = teacherRequest.ContactNo;
				existingTeacher.Designation = teacherRequest.Designation;
				existingTeacher.CreditToBeTaken = teacherRequest.CreditToBeTaken;

				_context.SaveChanges();

			}


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
