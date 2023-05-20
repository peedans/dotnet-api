using System.ComponentModel.DataAnnotations;
using dotnetapi.ViewModels.Base;

namespace dotnetapi.ViewModels.User
{
    public class ActionViewModel : BaseViewModel
    {
        public string? name { get; set; }
        public string? priority { get; set; }
    }

    public class ActionRequestViewModel
    {
        [Required]
        public string? name { get; set; }
        [Required]
        public string? priority { get; set; }
    }

    public class ActionResponseViewModel
    {
        public long id { get; set; }
        public string? name { get; set; }
        public string? priority { get; set; }
    }


}