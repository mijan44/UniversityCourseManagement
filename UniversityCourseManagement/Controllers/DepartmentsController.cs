using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityCourseManagement.Data;
using UniversityCourseManagement.Models;

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


		[HttpGet("{id}")]
		public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
		{
			return await _context.Departments.ToListAsync();
		}


		[HttpPost]
		public async Task<ActionResult<Department>> PostDepartment( Department department)
		{
			if (_context.Departments.Any(d => d.Code == department.Code || d.Name==department.Name)) 
			{
				return BadRequest("Department code or Name already exists");
			}

			_context.Departments.Add(department);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetDepartment", new { id = department.DepartmentID }, department);

		}

	}
}
