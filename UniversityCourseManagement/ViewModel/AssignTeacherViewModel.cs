using UniversityCourseManagement.Models;

namespace UniversityCourseManagement.ViewModel
{
	public class AssignTeacherViewModel
	{
		public Guid TeacherId { get; set; }
		public Guid CourseId { get; set; }
		public decimal AssignedCredit { get; set; }
		public decimal RemainingCredit { get; set; }
		public virtual Department Department { get; set; }
		
		

	}
}
