using System.ComponentModel.DataAnnotations;
using dotnetapi.ViewModels.Base;

namespace dotnetapi.ViewModels.User
{
    public class RoleViewModel : BaseViewModel
    {
        public string? name { get; set; }

        public string? description  { get; set; }

    }

    public class RoleRequestViewModel
    {
        [Required]
        public string? name { get; set; }

        public string? description  { get; set; }
    }

    public class RoleResponseViewModel
    {
        public long id { get; set; }
        public string? name { get; set; }
        public string? description  { get; set; }
        
        public List<ActionResponseViewModel>? actions { get; set; }
    }


}