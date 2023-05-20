using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NTR.Common.DataAccess.Entities;

namespace dotnetapi.Models.User
{
    public class UserCompany : EntityBase
    {

        [Column("user_id")]
        public long? UserId { get; set; }

        [Column("company_id")]
        public long? CompanyId { get; set; }

        [Column("is_main")]
        public bool IsMain { get; set; }


        public virtual User User { get; set; }

        public virtual Company Company { get; set; }

    }

}