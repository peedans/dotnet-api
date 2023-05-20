namespace dotnetapi.ViewModels.Paginations;

using dotnetapi.ViewModels.Filter;

public class PageRequestViewModel : FilterModel
{
    public PageRequestViewModel()
    {
        page_index = 1;
        item_per_page = 1000;
    }

    public int page_index { get; set; }

    public int item_per_page { get; set; }

    public int? item_total { get; set; }
}