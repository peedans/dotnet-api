using System.ComponentModel.DataAnnotations;
using dotnetapi.ViewModels.Base;

namespace dotnetapi.ViewModels.User
{
    public class SalaryHistoryViewModel : BaseViewModel
    {
        public int? amount { get; set; }

        public long salary_id { get; set;}

    }

    public class SalaryHistoryRequestViewModel
    {
        public int? amount { get; set; }

        public long salary_id { get; set;}
    }

    public class SalaryHistoryResponseViewModel
    {
        public long id { get; set; }
        public int? amount { get; set; }

        public SalaryResponseViewModel? salary { get; set; }
    }

    public class SalaryHistoryUpdateRequestViewModel
    {
        public int? amount { get; set; }

    }



}