using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using dotnetapi.Models.Entities;

namespace dotnetapi.Models.Master
{
	public class BlogPostTag
    {
        [Key]
        [Column("blog_post_id")]
        public long BlogPostId { get; set; }

        [Key]
        [Column("tag_id")]
        public long TagId { get; set; }

        public Tag? Tag { get; set; }
        public BlogPost? BlogPost { get; set; }
        public BlogPostTag()
		{
		}
	}
}

