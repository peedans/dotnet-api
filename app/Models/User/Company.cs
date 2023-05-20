using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NTR.Common.DataAccess.Entities;

namespace dotnetapi.Models.User
{
    public class Company : EntityBase
    {
        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("location")]
        [MaxLength(255)]
        public string Location { get; set; }

        public ICollection<UserCompany> UserCompany { get; set; }

    }
}

