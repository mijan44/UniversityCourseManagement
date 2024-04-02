namespace UniversityCourseManagement.Models
{
	public class StudentEnrollment
	{
		public Guid Id { get; set; }
		public string RegistrationNumber { get; set; }
		public virtual Course Course { get; set; }
		public DateTime Date { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}
