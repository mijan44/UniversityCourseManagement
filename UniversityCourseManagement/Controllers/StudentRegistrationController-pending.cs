﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityCourseManagement.Data;
using UniversityCourseManagement.Models;
using UniversityCourseManagement.ViewModel;

namespace UniversityCourseManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentRegistrationController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public StudentRegistrationController(ApplicationDbContext context)
		{
			_context = context;
		}
		[HttpGet]


		public async Task<ActionResult<RegisterStudent>> GetStudent(Guid id)
		{
			var result = await _context.RegisterStudents.Where(x => x.Id == id).ToListAsync();
			return Ok(result);
		}
		[HttpPost]
		public async Task<ActionResult<RegisterStudent>>PostStudent(StudentRegistrationViewModel studentRequest)
		{
			if(_context.RegisterStudents.Any(d => d.StudentName == studentRequest.StudentName || d.StudentContactNo == studentRequest.StudentContactNo))
			{
				return BadRequest("Student already exist");
			}


			var student = new RegisterStudent();
			student.Id = Guid.NewGuid();
			student.StudentName = studentRequest.StudentName;
			student.StudentEmail = studentRequest.StudentEmail;
			student.StudentContactNo = studentRequest.StudentContactNo;
			//student.DateTime = studentRequest.DateTime;
			student.StudentAddress = studentRequest.StudentAddress;
			//student.Department = studentRequest.Department;
			student.RegistrationNumber = studentRequest.RegistrationNumber;

			_context.RegisterStudents.Add(student);
			await _context.SaveChangesAsync();
			return CreatedAtAction("GetStudent", new { id = student.Id }, studentRequest);

		}

		[HttpPut]
		public IActionResult UpdateRegisterStudent (Guid id, RegisterStudent updateRegisterStudent)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var existingStudent = _context.RegisterStudents.FirstOrDefault(d => d.Id == id);
			{
				if (existingStudent == null)
				{
					return NotFound();
				}

				existingStudent.StudentName = updateRegisterStudent.StudentName;
				existingStudent.StudentEmail = updateRegisterStudent.StudentEmail;
				existingStudent.StudentContactNo= updateRegisterStudent.StudentContactNo;
				existingStudent.StudentAddress = updateRegisterStudent.StudentAddress;
				existingStudent.RegistrationNumber = updateRegisterStudent.RegistrationNumber;


				_context.SaveChanges();
				return Ok();
			}
			
		}

		private bool StudentAvailable(int id)
		{
			return _context.RegisterStudents.Any(id => _context.RegisterStudents.Any());
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteStudent(Guid id)
		{

			var student = await _context.RegisterStudents.Where(x => x.Id == id).FirstAsync();
			if (student == null)
			{
				return NotFound();
			}
			_context.RegisterStudents.Remove(student);

			await _context.SaveChangesAsync();

			return Ok();
		 }


	}
}