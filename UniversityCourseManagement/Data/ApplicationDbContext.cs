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

		public DbSet<Teacher> Teachers { get; set; }

		public DbSet<CourseAssignmentTeacher> CourseAssignmentTeachers { get; set; }

		public DbSet<ClassRoom> ClassRooms { get; set; }
		public DbSet<StudentEnrollment> StudentEnrollments { get; set; }
		public DbSet<Result> Results { get; set; }
		public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
		public DbSet<User> Users { get; set; }



	}
}
