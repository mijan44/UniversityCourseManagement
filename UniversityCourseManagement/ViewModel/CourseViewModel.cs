using UniversityCourseManagement.Models;

namespace UniversityCourseManagement.ViewModel
{
	public class CourseViewModel
	{
		public string CourseCode { get; set; }
		public string CourseName { get; set; }
		public double CourseCredit { get; set; }
		public string CourseDescription { get; set; }
		//
		public virtual Department Department { get; set; }
		public Guid SemesterID { get; set; }
		public virtual Semester Semester { get; set; }
		public Guid DepartmentID { get; set; }


	}
}
