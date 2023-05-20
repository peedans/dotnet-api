using dotnetapi.ViewModels.Base;

namespace dotnetapi.ViewModels.User
{
    public class UserViewModel : BaseViewModel
    {
        public string? username { get; set; }
		
        public string name { get; set; }
		
		public string lastname { get; set; }
		
        public string email { get; set; }
		
        public string? password { get; set; }
		
        public DateTime? last_login { get; set; }
		
        public DateTime? last_logout { get; set; }

        public long department_id { get; set;}

        public long role_id { get; set; }
		
    }

    public class UserRequestViewModel
    {
        public string? username { get; set; }
		
        public string name { get; set; }
		
		public string lastname { get; set; }
		
        public string email { get; set; }

        public string? password { get; set; }
        public long department_id { get; set;}

        public long role_id { get; set; }

		
    }


    public class UserResponseViewModel
    {
        public long id { get; set; }
        public string? username { get; set; }
        public string name { get; set; }
		public string lastname { get; set; }
        public string email { get; set; }
        public DepartmentResponseViewModel? department { get; set; }
        public RoleResponseViewModel? role { get; set; }

        public List<IsMainUserCompanyResponseViewModel>? company { get; set; }
		
    }

    public class UserRequestUpdateViewModel
    {
        public string name { get; set; }
		public string lastname { get; set; }
        public string email { get; set; }

        public string? password { get; set; }
		
    }
}
