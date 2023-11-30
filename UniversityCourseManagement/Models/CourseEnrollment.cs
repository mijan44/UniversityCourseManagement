namespace UniversityCourseManagement.Models
{
	public class CourseEnrollment
	{
		public Guid Id { get; set; }
		public string RegistrationNumber { get; set; }
		public Guid DepartmentId { get; set; }
		public Guid CourseId { get; set; }
		
	}
}