namespace UniversityCourseManagement.Models
{
	public class RegisterStudent
	{
		public Guid Id { get; set; }
		public string StudentName { get; set; }
		public string StudentEmail { get; set; }
		public string StudentContactNo { get; set; }
		public DateTime DateTime { get; set; }
		public string StudentAddress { get; set; }
		//public virtual Department Department { get; set; }
		public string RegistrationNumber { get; set; }
        public  Guid DepartmentId { get; set; }
		public bool IsDeleted { get; set; } = false;


	}
}
