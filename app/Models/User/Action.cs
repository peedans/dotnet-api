using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NTR.Common.DataAccess.Entities;

namespace dotnetapi.Models.User
{
    public class Action : EntityBase
    {
        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("priority")]
        [MaxLength(255)]
        public string Priority { get; set; }

        public ICollection<RoleAction> RoleAction { get; set; }
    }

}