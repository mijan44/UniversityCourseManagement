﻿namespace UniversityCourseManagement.ViewModel
{
	public class CourseEnrollmentViewModel
	{
		public Guid Id { get; set; }
		public string RegistrationNumber { get; set; }
		public DateTime EnrollmentDate { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
		public string  StudentsEmail { get; set; }
		public string DepartmentName { get; set; }
        public Guid CourseId { get; set; }
		public string CourseName { get;  set; }
		
	}
}
