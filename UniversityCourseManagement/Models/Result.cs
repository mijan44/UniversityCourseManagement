namespace UniversityCourseManagement.Models
{
	public class Result
	{
		public Guid Id { get; set; }
		public virtual RegisterStudent RegisterStudent { get; set; }
		public string GradeLetter { get; set; }
		public virtual Course Course { get; set; }
	}
}
