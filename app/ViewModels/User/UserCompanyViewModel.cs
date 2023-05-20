using System.ComponentModel.DataAnnotations;
using dotnetapi.ViewModels.Base;

namespace dotnetapi.ViewModels.User
{
    public class UserCompanyViewModel : BaseViewModel
    {
        public long? user_id { get; set; }
        public long? company_id { get; set; }
        public bool? is_main { get; set; }
    }

    public class UserCompanyRequestViewModel
    {
        [Required]
        [Range(1,long.MaxValue)]
        public long user_id { get; set; }
        [Required]
        [Range(1,long.MaxValue)]
        public long company_id { get; set; }

        public bool? is_main { get; internal set; } = false;
    }

    public class UserCompanyResponseViewModel
    {

        public long user_id { get; set; }
        public long company_id { get; set; }
        public bool? is_main { get; set; }
    }

    public class UserCompanyRequestUpdateViewModel
    {
        [Range(1,long.MaxValue)]
        public long user_id { get; set; }
        [Range(1,long.MaxValue)]
        public long company_id { get; set; }
		
    }

    public class IsMainUserCompanyResponseViewModel
    {
        public long id { get; set; }
        public string? name { get; set; }
        public string? location { get; set; }

        public bool? is_main { get; set; }
    }

    public class UserCompanyDeleteRequestViewModel
    {

        public long user_id { get; set; }
        public long company_id { get; set; }

    }
}