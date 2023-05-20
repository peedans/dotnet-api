using System.ComponentModel.DataAnnotations;
using dotnetapi.ViewModels.Base;

namespace dotnetapi.ViewModels.User
{
    public class SalaryViewModel : BaseViewModel
    {
        public string? description  { get; set; }
        public int? amount { get; set; }

        public long company_id { get; set;}
        public long user_id { get; set;}

    }

    public class SalaryRequestViewModel
    {
        public string? description  { get; set; }
        public int? amount { get; set; }

        public long company_id { get; set;}
        public long user_id { get; set;}
    }

    public class SalaryResponseViewModel
    {
        public long id { get; set; }
        public string? description  { get; set; }
        public int? amount { get; set; }
        public CompanyResponseViewModel? company { get; set; }
        public UserResponseViewModel? user { get; set; }

    }

     public class SalaryRequestUpdateViewModel
    {
        public string? description  { get; set; }
        public int? amount { get; set; }

    }



}