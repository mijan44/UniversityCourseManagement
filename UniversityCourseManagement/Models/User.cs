namespace UniversityCourseManagement.Models
{
	public class User
	{
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string UserEmail { get; set; }
        public string UserContact { get; set; }
        public string UserPassword { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}
