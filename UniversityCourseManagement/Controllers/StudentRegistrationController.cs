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
	public class StudentRegistrationController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public StudentRegistrationController(ApplicationDbContext context)
		{
			_context = context;
		}


		[HttpGet]
		[Route("GetStudentDetails")]
		public async Task<ActionResult<CourseEnrollmentViewModel>> GetStudent( string studentId)
		{
			var result = await _context.RegisterStudents.Where(x=>x.Id== new Guid(studentId)).FirstOrDefaultAsync();
			return Ok(result);
		}



		[HttpGet]
		public async Task<ActionResult<RegisterStudent>> GetStudent()
		{
			var result = await _context.RegisterStudents.ToListAsync();
			return Ok(result);
		}


		//[HttpGet]
		//public async Task<ActionResult<RegisterStudent>> GetStudent(Guid id)
		//{
		//	var result = await _context.RegisterStudents.Where(x => x.Id == id).ToListAsync();
		//	return Ok(result);
		//}


		[HttpPost]
		public async Task<ActionResult<RegisterStudent>>PostStudent(RegisterStudent studentRequest)
		{
			if (studentRequest.Id ==null || studentRequest.Id == new Guid("00000000-0000-0000-0000-000000000000"))
			{
				string registrationNumber;
				string departmentCode;
				int slNo = 0;


				var student = new RegisterStudent();
				student.Id = Guid.NewGuid();
				student.StudentName = studentRequest.StudentName;
				student.StudentEmail = studentRequest.StudentEmail;
				student.StudentContactNo = studentRequest.StudentContactNo;
				student.DateTime = System.DateTime.Today;
				student.StudentAddress = studentRequest.StudentAddress;
				student.DepartmentId = studentRequest.DepartmentId;
				departmentCode = _context.Departments.Where(x => x.Id == studentRequest.DepartmentId).Select(x=>x.Code).FirstOrDefault();
				var count = _context.RegisterStudents.Where(x => x.DepartmentId == studentRequest.DepartmentId).Count();
				if (count > 0)
				{
					//slNo = _context.RegisterStudents.Where(x => x.Id == studentRequest.DepartmentId).Max(x => Convert.ToInt16(x.RegistrationNumber.Substring(12)))+1;
					slNo = (from max in _context.RegisterStudents where max.DepartmentId == studentRequest.DepartmentId select Convert.ToInt16(max.RegistrationNumber.Substring(12))+1).Max();
				}
				else
				{
					slNo = 1;
				}
				
				registrationNumber = departmentCode + "-" + student.DateTime.Year.ToString() + "-" + slNo.ToString("0000");
				student.RegistrationNumber = registrationNumber;


				_context.RegisterStudents.Add(student);
				await _context.SaveChangesAsync();

			}
			else
			{
				var existingStudent = _context.RegisterStudents.FirstOrDefault(d => d.Id == studentRequest.Id);
				{
					if (existingStudent == null)
					{
						return NotFound();
					}

					existingStudent.StudentName = studentRequest.StudentName;
					existingStudent.StudentEmail = studentRequest.StudentEmail;
					existingStudent.StudentContactNo = studentRequest.StudentContactNo;
					existingStudent.StudentAddress = studentRequest.StudentAddress;
					existingStudent.RegistrationNumber = studentRequest.RegistrationNumber;


					_context.SaveChanges();
					
				}

			}
		


			return Ok();
			

		}

		//[HttpPut]
		//public IActionResult UpdateRegisterStudent (Guid id, RegisterStudent updateRegisterStudent)
		//{
		//	if(!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}
		//	var existingStudent = _context.RegisterStudents.FirstOrDefault(d => d.Id == id);
		//	{
		//		if (existingStudent == null)
		//		{
		//			return NotFound();
		//		}

		//		existingStudent.StudentName = updateRegisterStudent.StudentName;
		//		existingStudent.StudentEmail = updateRegisterStudent.StudentEmail;
		//		existingStudent.StudentContactNo= updateRegisterStudent.StudentContactNo;
		//		existingStudent.StudentAddress = updateRegisterStudent.StudentAddress;
		//		existingStudent.RegistrationNumber = updateRegisterStudent.RegistrationNumber;


		//		_context.SaveChanges();
		//		return Ok();
		//	}
			
		//}

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
