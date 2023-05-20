using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NTR.Common.DataAccess.Entities;

namespace dotnetapi.Models.User
{
    public class Department : EntityBase
    {
        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        public ICollection<User> User { get; set; }

    }

}