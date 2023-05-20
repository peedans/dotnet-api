using System.ComponentModel.DataAnnotations;
using dotnetapi.ViewModels.Base;

namespace dotnetapi.ViewModels.User
{
    public class DepartmentViewModel : BaseViewModel
    {
        public string? name { get; set; }

    }

    public class DepartmentRequestViewModel
    {
        [Required]
        public string? name { get; set; }
    }

    public class DepartmentResponseViewModel
    {
        public long id { get; set; }
        public string? name { get; set; }
    }


}