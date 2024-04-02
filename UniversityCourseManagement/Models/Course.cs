using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UniversityCourseManagement.Models
{
	public class Course
	{
		
		public Guid Id { get; set; }
		//[Required]
		//[StringLength(7, MinimumLength = 5)]
		
		public string CourseCode { get; set; }

		//[Required]
		//[MaxLength(255)]
		public string CourseName { get; set; }

		//[Range(0.5, 5.0, ErrorMessage = "Credit must be between 0.5 and 5.0.")]
		public double CourseCredit { get; set; }
		public string CourseDescription { get; set; }
		public Guid DepartmentID { get; set; }
		//public virtual Department Department { get; set; }
	
		public int Semester { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}
