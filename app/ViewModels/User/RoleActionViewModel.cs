using System.ComponentModel.DataAnnotations;
using dotnetapi.ViewModels.Base;

namespace dotnetapi.ViewModels.User
{
    public class RoleActionViewModel : BaseViewModel
    {
        public long? roleId { get; set; }
        public long? actionId { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class RoleActionRequestViewModel
    {
        [Required]
        public long? roleId { get; set; }
        [Required]
        public long? actionId { get; set; }
    }

    public class RoleActionResponseViewModel
    {
        public long? roleId { get; set; }
        public long? actionId { get; set; }

    }


}