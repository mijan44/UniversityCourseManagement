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
									r.IsDeleted
									
								}).ToListAsync();

			return Ok(result.Where(x=>!x.IsDeleted));
		}


		[HttpPost]
		public async Task<ActionResult<Result>> PostResult(Result request)
		{
			var existResult = _context.Results.FirstOrDefault(x=>x.StudentId == request.StudentId  &&  x.CourseId == request.CourseId && !x.IsDeleted);
			if (existResult == null)
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
					return Ok(new { ststusCode = 200, message = " Student's Result Saved SuccessFully" });

				}
				


			}
			else
			{
				var existingResult = _context.Results.FirstOrDefault(d => d.Id == request.Id);
				{
					

					existingResult.GradeLetter = request.GradeLetter;


					_context.SaveChanges();
					return Ok(new { ststusCode = 200, message = " Student's Result Updated SuccessFully" });

				}

			}


			return Ok("Students Result Alreaddy Exist");

		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteResult(Guid id)
		{

			var result = await _context.Results.Where(x => x.Id == id).FirstAsync();
			if (result == null)
			{
				return NotFound(new { ststusCode = 204, message = " Student's Result Deleted Failed" });
			}
			result.IsDeleted = true;
			_context.Results.Update(result);

			await _context.SaveChangesAsync();

			return Ok(new { ststusCode = 204, message = " Student's Result Deleted SuccessFully" });
		}


	}
}
