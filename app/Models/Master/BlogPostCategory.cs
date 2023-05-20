using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using dotnetapi.Models.Master;
using NTR.Common.DataAccess.Entities;

namespace app.Models.Master
{
    public class BlogPostCategory : EntityBase
    {
     
        [Column("blogpost_id ")]
        public long BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
     
        [Column("category_id ")]
        public long CategoryId { get; set; }
        public Category Category { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }
       
    }
}