using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using app.Models.Master;
// using dotnetapi.Models.Entities;
using NTR.Common.DataAccess.Entities;


namespace dotnetapi.Models.Master
{
	public class BlogPost : EntityBase
    {
        [Column("title")]
        [MaxLength(255)]
        public string? Title { get; set; }
        [Column("content")]
        [MaxLength(255)]
        public string? Content { get; set; }
        [Column("cover_image")]
        [MaxLength(255)]
        public string? CoverImage { get; set; }
        [Column("author_id")]
        public long AuthorId { get; set; }
        public Author? Author { get; set; }

        public ICollection<BlogPostTag>? BlogPostTags { get; set; }

        public ICollection<BlogPostCategory>? BlogPostCategory { get; set; }

        public BlogPost()
		{
		}
	}
}

