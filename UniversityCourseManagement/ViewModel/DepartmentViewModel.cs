namespace UniversityCourseManagement.ViewModel
{
	public class DepartmentViewModel
	{
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
		public DateTime InsertedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

	}
}
