using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NTR.Common.DataAccess.Entities {
    public class EntityBase {
        [Comment("Primary Key")]
        [Key]
        public long id { get; set; }
        [Comment("Flag Soft Delete")]
        public bool is_deleted { get; set; }
        [Comment("Audit column, created date")]
        public DateTime? created_date { get; set; }
        [Comment("Audit column, created by")]
        public long? created_by { get; set; }
        [Comment("Audit column, updated date")]
        public DateTime? updated_date { get; set; }
        [Comment("Audit column, updated by")]
        public long? updated_by { get; set; }
    }
}
