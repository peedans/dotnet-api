using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapi.ViewModels.Base;

namespace app.ViewModels.Master
{
    public class CategoryViewModel : BaseViewModel
    {
        public string? name { get; set; }
        public string? description  { get; set; }
    }

    public class CategoryRequestViewModel
    {
        public string? name { get; set; }
        public string? description  { get; set; }

    }

    public class CategoryResponseViewModel
    {
        public long id { get; set; }
        public string? name { get; set; }
        public string? description  { get; set; }
    
    }

    public class IsActiveCategoryResponseViewModel
    {
        public long id { get; set; }
        public string? name { get; set; }
        public string? description  { get; set; }
        public bool? is_active { get; set; }
    }

}