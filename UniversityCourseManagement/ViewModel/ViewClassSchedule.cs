using System.Runtime.CompilerServices;

namespace UniversityCourseManagement.ViewModel
{
	public class ViewClassSchedule
	{
        public string CourseCode { get; set; }
        public string  CourseName { get; set; }
        public string ScheduleInfo { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
