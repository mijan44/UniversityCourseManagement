namespace UniversityCourseManagement.ViewModel
{
	public class ClassRoomViewModel
	{
		public Guid Id { get; set; }
		public int RoomNo { get; set; }
		public string Day { get; set; }
		//public Guid DayId { get; set; }
		public DateTime? From { get; set; }
		public DateTime? To { get; set; }
		public Guid CourseId { get; set; }
		public Guid DepartmentId { get; set; }
		public string FromTime { get; set; }
		public string FromAmPm { get; set; }
		public string ToTime { get; set; }
		public string ToAmPm { get; set; }
	}
}
