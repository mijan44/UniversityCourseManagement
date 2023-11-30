namespace UniversityCourseManagement.Models
{
	public class ClassRoom
	{
		public Guid Id { get; set; }
		public int RoomNo { get; set; }
		public string Day { get; set;}
		//public Guid DayId { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public Guid CourseId { get; set; }
		public Guid DepartmentId { get; set; }
		//public virtual Course Course { get; set; }
		//public virtual Department Department { get; set; }
		//public Guid DepartmentId { get; set; }
	}
}
