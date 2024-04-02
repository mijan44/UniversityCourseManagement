namespace UniversityCourseManagement.ViewModel
{
	public class ResultViewModel
	{
		public Guid Id { get; set; }
		public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public string  StudentEmail { get; set; }
        public string DepartmentName { get; set; }
        public string GradeLetter { get; set; }
		public Guid CourseId { get; set; }
        public string  CourseName { get; set; }
        public string RegistrationNumber { get; set; }
        public bool IsDeleted { get; set; }

      
    }
}
