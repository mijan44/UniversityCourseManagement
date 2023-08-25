using System.ComponentModel.DataAnnotations;

namespace UniversityCourseManagement.Models
{
	public class Department
	{
		public Guid Id { get; set; }
		[Required]
		[MaxLength(7)]
		
		public string Code { get; set; }
		[Required]
		[MaxLength(255)]
		public string Name { get; set; }
		public DateTime InsertedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsDeleted { get; set; }


	}
}
