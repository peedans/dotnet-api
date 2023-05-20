namespace dotnetapi.ViewModels.Base;

public class BaseViewModel
{
    public int id { get; set; }
    public bool is_deleted { get; set; } = false;
    public DateTime? created_date { get; set; }
    public int? created_by { get; set; }
    public DateTime? updated_date { get; set; }
    public int? updated_by { get; set; }
}