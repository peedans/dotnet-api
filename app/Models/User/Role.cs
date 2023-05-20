using System.ComponentModel.DataAnnotations.Schema;
using NTR.Common.DataAccess.Entities;

namespace dotnetapi.Models.User{
    public class Role : EntityBase
    {
        [Column("name")]
        public string? Name { get; set; }
        [Column("description")]
        public string? Description { get; set; }

        public ICollection<User>? Users { get; set; }

        public ICollection<RoleAction>? RoleActions { get; set; }
    }
}