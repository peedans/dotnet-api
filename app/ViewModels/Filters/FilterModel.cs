namespace dotnetapi.ViewModels.Filter;

 public class FilterModel
{
    public string order_by { get; set; }
    public bool is_order_reverse { get; set; }
    public SearchTextModel? search_text { get; set; }
    public FilterModel()
    {
        order_by = "id";
        is_order_reverse = false;
    }
}

public class SearchTextModel
{
    public List<string>? fields { get; set; }
    public string? input_text { get; set; }
}