using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Linq;
using UniversityCourseManagement.Data;
using UniversityCourseManagement.Helpers;
using UniversityCourseManagement.Models;
using UniversityCourseManagement.ViewModel;

namespace UniversityCourseManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public DepartmentsController(ApplicationDbContext context)
		{
			_context = context;
		}


		//[HttpGet("{id:Guid}")]
		//public async Task<ActionResult<Department>>  GetDepartments(Guid id)
		//{
		//	var result = await _context.Departments.Where(x=>x.Id == id).ToListAsync();
		//	return Ok(result);
		//}

		

		[HttpGet]
		public async Task<ActionResult<Department>> GetDepartments()
		{
			var result = await _context.Departments.Where(x=>!x.IsDeleted).ToListAsync();
			return Ok(result);
		}


		[HttpPost]
		public async Task<ActionResult<Department>> PostDepartment( DepartmentViewModel departmentRequest)
		
		{
			var existDepartment = _context.Departments.FirstOrDefault(x=>x.Name == departmentRequest.Name && x.Code == departmentRequest.Code && !x.IsDeleted);
			if (existDepartment == null)
			{
				if (departmentRequest.Id == null || departmentRequest.Id == new Guid("00000000-0000-0000-0000-000000000000"))
				{
					Validation validation = new Validation();
					if(validation.CheckSpecialChar(departmentRequest.Code) || validation.CheckSpecialChar(departmentRequest.Name))
					return Ok(new { ststusCode = 200, message = departmentRequest.Name + " - Special Character Not Allowed" });
					

					var department = new Department();
					department.Id = Guid.NewGuid();
					department.Code = departmentRequest.Code;
					department.Name = departmentRequest.Name; 
					department.InsertedAt = DateTime.Now;
					

					_context.Departments.Add(department);
					await _context.SaveChangesAsync();
					return Ok(new { ststusCode = 200, message = department.Name + " Department Saved SuccessFully" });

				}
			

			}
			else
			{
				var existingDepartment = _context.Departments.FirstOrDefault(d => d.Id == departmentRequest.Id);

				// Update the properties of the existing department
				existingDepartment.Code = departmentRequest.Code;
				existingDepartment.Name = departmentRequest.Name;
				existingDepartment.UpdatedAt = DateTime.Now; // Update the UpdatedAt timestamp

				// Save changes to the database
				_context.SaveChanges();
				return Ok(new { ststusCode = 200, message = existingDepartment.Name + " Department Updated Successfully" });
			}

			return Ok();

			

		}

		


		private bool DepartmentAvailable(int id)
		{
			return _context.Departments.Any(id
				=> _context.Departments.Any());
		}
		///



		/// Delete Departments

		[HttpDelete("{id}")]
		public async Task<IActionResult>DeleteDepartment(Guid id )
		{
			
			var department = await _context.Departments.Where(x=>x.Id==id).FirstAsync();
			if (department ==null)
			{
				return NotFound();
			}
			department.IsDeleted = true;
			_context.Departments.Update(department);

			await _context.SaveChangesAsync();

			return Ok(new { ststusCode = 200, message = department.Name + " Department Deleted Successfully" });
		}



	}
}
