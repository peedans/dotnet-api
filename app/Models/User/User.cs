using System;
using System.ComponentModel.DataAnnotations.Schema;
using NTR.Common.DataAccess.Entities;

namespace dotnetapi.Models.User
{
	public class User : EntityBase
    {
		[Column("username")]
        public string? Username { get; set; }
		[Column("name")]
        public string Name { get; set; }
		[Column("lastname")]
		public string Lastname { get; set; }
		[Column("email")]
        public string Email { get; set; }
		[Column("password")]
        public string? Password { get; set; }
		[Column("last_login")]
        public DateTime? LastLogin { get; set; }
		[Column("last_logout")]
        public DateTime? LastLogout { get; set; }

		[Column("department_id")]
		[ForeignKey("Department")]
        public long DepartmentId { get; set;}
		public Department? Department { get; set;}

		[Column("role_id")]
		[ForeignKey("Role")]
        public long RoleId { get; set; }
		public Role? Role { get; set;}

		public ICollection<Salary>? Salary { get; set; }

		public ICollection<UserCompany>? UserCompany { get; set; }
    }
}

