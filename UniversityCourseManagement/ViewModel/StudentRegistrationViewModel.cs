using UniversityCourseManagement.Models;

namespace UniversityCourseManagement.ViewModel
{
	public class StudentRegistrationViewModel
	{
		public string StudentName { get; set; }
		public string StudentEmail { get; set; }
		public string StudentContactNo { get; set; }
		//public DateTime DateTime { get; set; }
		public string StudentAddress { get; set; }
		//public virtual Department Department { get; set; }
		public int RegistrationNumber { get; set; }

	}
}
