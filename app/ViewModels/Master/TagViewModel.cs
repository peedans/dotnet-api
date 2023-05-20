using dotnetapi.ViewModels.Base;
using dotnetapi.ViewModels.Paginations;

namespace dotnetapi.ViewModels.Master
{
    public class TagViewModel : BaseViewModel
    {
        public string? name { get; set; }
    }

    public class TagRequestViewModel
    {
        public string? name { get; set; }
    }


    public class TagResponseViewModel
    {
        public long? id { get; set;}
        public string? name { get; set; }
    }

    public class TagRequestPageViewModel : PageRequestViewModel
    {
        // public FilterTagViewModel? filter { get; set; }
    }

    public class FilterTagViewModel
    {
        public string? name { get; set; }
    }
}