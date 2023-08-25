using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Linq;
using UniversityCourseManagement.Data;
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


		[HttpGet("{id:Guid}")]
		public async Task<ActionResult<Department>>  GetDepartments(Guid id)
		{
			var result = await _context.Departments.Where(x=>x.Id == id).ToListAsync();
			return Ok(result);
		}


		[HttpPost]
		public async Task<ActionResult<Department>> PostDepartment( DepartmentViewModel departmentRequest)
		
		{
			if (_context.Departments.Any(d => d.Code == departmentRequest.Code || d.Name==departmentRequest.Name)) 
			{
				return BadRequest("Department code or Name already exists");
			}

			var department = new Department();
			department.Id = Guid.NewGuid();
			department.Code = departmentRequest.Code;
			department.Name = departmentRequest.Name;

			_context.Departments.Add(department);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetDepartment", new { id = department.Id }, departmentRequest);

		}

		

		/// Update Departments
		[HttpPut]
		public IActionResult UpdateDepartment(Guid id, Department updatedDepartment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Find the department by ID
			var existingDepartment = _context.Departments.FirstOrDefault(d => d.Id == id);

			if (existingDepartment == null)
			{
				return NotFound();
			}

			// Update the properties of the existing department
			existingDepartment.Code = updatedDepartment.Code;
			existingDepartment.Name = updatedDepartment.Name;
			existingDepartment.UpdatedAt = DateTime.Now; // Update the UpdatedAt timestamp

			// Save changes to the database
			_context.SaveChanges();

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
			
			var departyment = await _context.Departments.Where(x=>x.Id==id).FirstAsync();
			if (departyment ==null)
			{
				return NotFound();
			}
			_context.Departments.Remove(departyment);

			await _context.SaveChangesAsync();

			return Ok();
		}



	}
}
