namespace UniversityCourseManagement.ViewModel
{
	public class CourseEnrollmentViewModel
	{
		public Guid Id { get; set; }
		public string RegistrationNumber { get; set; }
		public Guid DepartmentId { get; set; }
		public Guid CourseId { get; set; }
	}
}
