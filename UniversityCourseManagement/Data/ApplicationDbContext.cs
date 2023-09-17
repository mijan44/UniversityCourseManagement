using Microsoft.EntityFrameworkCore;
using UniversityCourseManagement.Models;

namespace UniversityCourseManagement.Data
{
	public class ApplicationDbContext: DbContext
	{
		

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			this.ChangeTracker.LazyLoadingEnabled = false;
		}
		public DbSet<Department> Departments { get; set; }
		public DbSet<Course> Courses { get; set; }
		
		public DbSet<RegisterStudent> RegisterStudents { get; set; }
		public DbSet<Semester> Semesters { get; set; }
		public DbSet<Teacher> Teachers { get; set; }

		public DbSet<CourseAssignmentTeacher> CourseAssignmentTeachers { get; set; }




	}
}
