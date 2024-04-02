namespace UniversityCourseManagement.Models
{
	public class CourseEnrollment
	{
		public Guid Id { get; set; }
		public DateTime EnrollmentDate { get; set; }
		public Guid CourseId { get; set; }
		public Guid StudentId { get; set; }
		public bool IsDeleted { get; set; } = false;

	}
}