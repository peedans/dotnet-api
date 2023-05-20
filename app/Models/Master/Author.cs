using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using dotnetapi.Models.Entities;
using NTR.Common.DataAccess.Entities;

namespace dotnetapi.Models.Master
{
    public class Author : EntityBase
    {
        [Column("name")]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Column("email_address")]
        [MaxLength(255)]
        public string? EmailAddress { get; set; }

        public ICollection<BlogPost>? BlogPost { get; set; }

        public Author()
		{
		}
	}
}

