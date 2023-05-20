using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NTR.Common.DataAccess.Entities;

namespace dotnetapi.Models.User
{
    public class RoleAction : EntityBase
    {
        [Column("role_id ")]
        [Key]
        public long RoleId  { get; set; }

        [Column("action_id ")]
        [Key]
        public long ActionId  { get; set; }

        public Role Role { get; set; }

        public Action Action { get; set; }

    }

}