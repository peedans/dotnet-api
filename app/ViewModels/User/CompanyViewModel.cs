using System.ComponentModel.DataAnnotations;
using dotnetapi.ViewModels.Base;

namespace dotnetapi.ViewModels.User
{
    public class CompanyViewModel : BaseViewModel
    {
        public string? name { get; set; }
        public string? location { get; set; }
    }

    public class CompanyRequestViewModel
    {
        [Required]
        public string? name { get; set; }
        [Required]
        public string? location { get; set; }
    }

    public class CompanyResponseViewModel
    {
        public long id { get; set; }
        public string? name { get; set; }
        public string? location { get; set; }
    }


}