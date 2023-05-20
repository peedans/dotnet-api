using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace app.ViewModels.Master
{
    public class BlogPostCategoryViewModel
    {
        public long blogpost_id { get; set; }
        public long category_id { get; set; }
        public bool? is_active { get; set; }
    }

    public class BlogPostCategoryRequestActiveViewModel
    {
        [Range(1, long.MaxValue)]
        public long blogpost_id { get; set; }
        [Range(1, long.MaxValue)]
        public long category_id { get; set; }
    }

}