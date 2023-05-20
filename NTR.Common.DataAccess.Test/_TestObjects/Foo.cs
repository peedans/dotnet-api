using NTR.Common.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTR.Common.DataAccess.UnitTest._TestObjects
{
    public class Foo : EntityBase
    {
        //public bool is_deleted { get; set; }
        public DateTime created_date { get; set; }
        public long created_by { get; set; }
        public DateTime? updated_date { get; set; }
        public long? updated_by { get; set; }
    }
}
