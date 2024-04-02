namespace UniversityCourseManagement.Models
{
	public class Result
	{
		public Guid Id { get; set; }
		public Guid StudentId { get; set; }
		public string GradeLetter { get; set; }
		public Guid CourseId { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}
