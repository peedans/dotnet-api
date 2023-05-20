
using System.ComponentModel.DataAnnotations.Schema;
using NTR.Common.DataAccess.Entities;

namespace dotnetapi.Models.User
{
    public class SalaryHistory : EntityBase
    {
        [Column("salary_id")]
        [ForeignKey("salary")]
        public long SalaryId { get; set; }

        [Column("amount")]
        public float Amount { get; set; }

        public Salary Salary { get; set; }

    }
}