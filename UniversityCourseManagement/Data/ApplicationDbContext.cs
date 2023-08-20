using Microsoft.EntityFrameworkCore;
using UniversityCourseManagement.Models;

namespace UniversityCourseManagement.Data
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Department> Departments { get; set; }
	}
}
