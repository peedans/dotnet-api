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
    public class Category : EntityBase
    {
        [Required]
        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [MaxLength(255)]
        [Column("description")]
        public string Description { get; set; }

        public ICollection<BlogPostCategory>? BlogPostCategory { get; set; }
    }
}