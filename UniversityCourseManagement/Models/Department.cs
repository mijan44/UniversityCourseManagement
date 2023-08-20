using System.ComponentModel.DataAnnotations;

namespace UniversityCourseManagement.Models
{
	public class Department
	{
		public Guid DepartmentID { get; set; } = Guid.NewGuid();
		[Required]
		[MaxLength(7)]
		
		public string Code { get; set; }
		[Required]
		[MaxLength (255)]
		public string Name { get; set; }


	}
}
