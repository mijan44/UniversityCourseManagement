using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityCourseManagement.Data;
using UniversityCourseManagement.Helpers;
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
		public async Task<ActionResult<TeacherViewModel>> GetTeacher() 
		{
			
			var result = from t in _context.Teachers
						 join d in _context.Departments on t.DepartmentId equals d.Id
						 select new
						 {
							 t.TeacherName,
							 t.TeacherAddress,
							 t.TeacherEmail,
							 t.ContactNo,
							 t.Designation,
							 DepartmentId = d.Id,
							 DepartmentName = d.Name,
							 t.Id,
							 t.CreditToBeTaken,
							 t.IsDeleted

						 };
			return Ok(result.Where(x=>!x.IsDeleted));
		}



		[HttpPost]
		public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacherRequest)
		{
			var existTeacher = _context.Teachers.FirstOrDefault(x=>x.TeacherEmail == teacherRequest.TeacherEmail && x.ContactNo == teacherRequest.ContactNo && !x.IsDeleted);

			if (existTeacher == null)
			{
				if (teacherRequest.Id == null || teacherRequest.Id == new Guid("00000000-0000-0000-0000-000000000000"))
				{
					Validation validation = new Validation();
					if(validation.CheckSpecialChar(teacherRequest.TeacherName))
					return Ok(new { ststusCode = 200, message = teacherRequest.TeacherName + " - Special Character Not Allowed" });
					
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
					return Ok(new { ststusCode = 200, message = teacher.TeacherName + " Teacher Saved SuccessFully" });

				}
				

			}
			else
			{
				var existingTeacher = _context.Teachers.FirstOrDefault(t => t.Id == teacherRequest.Id);
				

				existingTeacher.TeacherName = teacherRequest.TeacherName;
				existingTeacher.TeacherAddress = teacherRequest.TeacherAddress;
				existingTeacher.TeacherEmail = teacherRequest.TeacherEmail;
				existingTeacher.ContactNo = teacherRequest.ContactNo;
				existingTeacher.Designation = teacherRequest.Designation;
				existingTeacher.CreditToBeTaken = teacherRequest.CreditToBeTaken;

				_context.SaveChanges();
				return Ok(new { ststusCode = 204, message = existingTeacher.TeacherName + " Teacher Updated SuccessFully" });

			}



			return Ok("Already Exist Teacher");
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
				return NotFound(new { ststusCode = 204, message =  " Teacher Deleted Failed" });
			}
			teacher.IsDeleted = true;
			_context.Teachers.Update(teacher);

			await _context.SaveChangesAsync();

			return Ok(new { ststusCode = 200, message =  " Teacher Deleted SuccessFully" });
		}



	}



}
