using System.ComponentModel.DataAnnotations;

namespace UniversityCourseManagement.Models
{
	public class Department
	{
		public Guid Id { get; set; }
		

		public string Code { get; set; }
		
		public string Name { get; set; }
		public DateTime InsertedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsDeleted { get; set; } = false;


	}
}
