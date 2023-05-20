using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using dotnetapi.Models.Entities;
using NTR.Common.DataAccess.Entities;


namespace dotnetapi.Models.Master
{
	public class Tag : EntityBase
    {
        [Column("name")]
        [MaxLength(255)]
        public string? Name { get; set; }

        public ICollection<BlogPostTag>? BlogPostTags { get; set; }
        
        public Tag()
		{
		}
	}
}

