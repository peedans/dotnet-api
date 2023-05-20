using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapi.Models.Entities
{
	public class EntityBase
	{
        [Comment("Primary Key")]
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Comment("Flag Soft Delete")]
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Comment("Audit column, created date")]
        [Column("created_date")]
        public DateTime? CreatedDate { get; set; }

        [Comment("Audit column, created by")]
        [Column("created_by")]
        public long? CreatedBy { get; set; }

        [Comment("Audit column, updated date")]
        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }

        [Comment("Audit column, updated by")]
        [Column("updated_by")]
        public long? UpdatedBy { get; set; }
	}
}

