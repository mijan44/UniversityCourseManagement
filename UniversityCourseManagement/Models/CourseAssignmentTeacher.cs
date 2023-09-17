namespace UniversityCourseManagement.Models
{
	public class CourseAssignmentTeacher
	{
		public Guid Id { get; set; }
		public Guid TeacherId { get; set; }
		public Guid CourseId { get; set; }
		public virtual Department Department { get; set; }
		
		public decimal AssignedCredit { get; set; }
		public decimal RemainingCredit { get; set; }
		//public string CourseCode { get; set; }
		//public string CourseName { get; set; }
		//public string CourseCredit { get; set; }
		
	}
}
