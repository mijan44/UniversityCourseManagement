using UniversityCourseManagement.Models;

namespace UniversityCourseManagement.ViewModel
{
	public class TeacherViewModel
	{
		public string TeacherName { get; set; }
		public string TeacherAddress { get; set; }
		public string TeacherEmail { get; set; }
		public string ContactNo { get; set; }
		public string Designation { get; set; }
		public string DepartmentName  { get; set; }
		public Guid DepartmentId { get; set; }

		public int CreditToBeTaken { get; set; }
	}
}
