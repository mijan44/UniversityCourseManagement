using UniversityCourseManagement.Models;

namespace UniversityCourseManagement.ViewModel
{
	public class AssignTeacherViewModel
	{
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
		public String TeacherName { get; set; }
		public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal CourseCredit { get; set; }
        public Guid DepartmentId { get; set; }

		public string DepartmentName { get; set; }
        public decimal AssignedCredit { get; set; }
		public decimal RemainingCredit { get; set; }
		
		

	}
}
