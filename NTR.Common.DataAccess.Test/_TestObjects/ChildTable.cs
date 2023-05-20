using NTR.Common.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTR.Common.DataAccess.Test._TestObjects
{
    public class ChildTable : EntityBase
    {
        public int parent_table_id { get; set; }
        //public bool is_deleted { get; set; }
        public DateTime created_date { get; set; }
        public long created_by { get; set; }
        public DateTime? updated_date { get; set; }
        public long? updated_by { get; set; }
    }
}
