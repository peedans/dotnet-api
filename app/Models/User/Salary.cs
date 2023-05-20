using System.ComponentModel.DataAnnotations.Schema;
using NTR.Common.DataAccess.Entities;

namespace dotnetapi.Models.User
{
    public class Salary : EntityBase
    {
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("company_id")]
        [ForeignKey("company")]
        public long CompanyId { get; set; }

        [Column("description")]
        public string? Description { get; set; }
        [Column("amount")]
        public float Amount { get; set; }

        public User User { get; set; }

        public ICollection<SalaryHistory> SalaryHistory { get; set; }

        public Company Company { get; set; }
    }
}