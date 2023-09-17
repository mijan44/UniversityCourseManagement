namespace UniversityCourseManagement.Models
{
	public class Teacher
	{
		public Guid Id { get; set; }
		public string TeacherName { get; set; }
		public string TeacherAddress { get; set; }
		public string TeacherEmail { get; set; }
		public string ContactNo { get; set; }
		public string Designation { get; set; }
		public virtual Department Department { get; set; }
		public Guid DepartmentId { get; set; }	

		public int CreditToBeTaken { get; set;}
		
	}
}
