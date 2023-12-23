namespace UniversityCourseManagement.ViewModel
{
	public class ClassRoomViewModel
	{
		public Guid Id { get; set; }
		public int RoomNo { get; set; }
		public string Day { get; set; }
		//public Guid DayId { get; set; }
		public string  From { get; set; }
		public string To { get; set; }
		public Guid CourseId { get; set; }
		public Guid DepartmentId { get; set; }
        public string  DepartmentName { get; set; }
        public string  CourseName { get; set; }
    }
}
