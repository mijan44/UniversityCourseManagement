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
	public class StudentsResultController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public StudentsResultController(ApplicationDbContext context)
		{
			_context = context;
		}


		[HttpGet]
		[Route("GetAllResults")]
		public async Task<ActionResult<ResultViewModel>> GetAllResults()
		{
			var result = await (from r in _context.Results
								join s in _context.RegisterStudents on r.StudentId equals s.Id
								join d in _context.Departments on s.DepartmentId equals d.Id
								join c in _context.Courses on r.CourseId equals c.Id
								select new
								{
									r.Id,
									r.StudentId,
									StudentName = s.StudentName,
									StudentEmail = s.StudentEmail,
									DepartmentId = s.DepartmentId,
									DepartmentName = d.Name,
									r.CourseId,
									s.RegistrationNumber,
									CourseName = c.CourseName,
									r.GradeLetter,
									
								}).ToListAsync();

			return Ok(result);
		}


		[HttpPost]
		public async Task<ActionResult<Result>> PostResult(Result request)
		{
			if (request.Id == null || request.Id == new Guid("00000000-0000-0000-0000-000000000000"))
			{
				var result = new Result();
				result.Id = Guid.NewGuid();
				result.StudentId = request.StudentId;
				result.CourseId = request.CourseId;
				result.GradeLetter = request.GradeLetter;
				




				_context.Results.Add(result);

				await _context.SaveChangesAsync();

			}
			else
			{
				var existingResult = _context.Results.FirstOrDefault(d => d.Id == request.Id);
				{
					if (existingResult == null)
					{
						return NotFound();
					}

					existingResult.GradeLetter = request.GradeLetter;
					

					_context.SaveChanges();

				}

			}


			return Ok();

		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteResult(Guid id)
		{

			var result = await _context.Results.Where(x => x.Id == id).FirstAsync();
			if (result == null)
			{
				return NotFound();
			}
			_context.Results.Remove(result);

			await _context.SaveChangesAsync();

			return Ok();
		}


	}
}
