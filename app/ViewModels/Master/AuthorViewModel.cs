using System.ComponentModel.DataAnnotations;
using dotnetapi.ViewModels.Base;
using dotnetapi.ViewModels.Paginations;

namespace dotnetapi.ViewModels.Master
{
    public class AuthorViewModel : BaseViewModel
    {
        public string? name { get; set; }
        public string? email_address { get; set; }
    }

    public class AuthorRequestViewModel
    {
        [Required]
        public string? name { get; set; }
        [Required]
        [EmailAddress]
        public string? email_address { get; set; }
    }

    public class AuthorResponseViewModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? email_address { get; set; }
    }

    public class AuthorRequestPageViewModel : PageRequestViewModel
    {
        public FilterAuthorViewModel? filter { get; set; }
    }

    public class FilterAuthorViewModel
    {
        public string? name { get; set; }
        public string? email_address { get; set; }

    }
}
