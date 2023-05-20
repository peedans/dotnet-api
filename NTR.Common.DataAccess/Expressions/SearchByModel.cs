using System;
using System.Collections.Generic;
using System.Text;

namespace NTR.Common.DataAccess.Expressions
{
    public class SearchByModel
    {
        public Operation Operation { get; set; }
        public string Key { get; set; }
        /// <summary>
        /// It is not used. Will be deleted.
        /// </summary>
        public string Value { get; set; }
        public List<string> Values { get; set; }

    }

    public class SearchTextModel
    {
        public List<string> Keys { get; set; }
        public string InputText { get; set; }
        public Operation Operation { get; set; }

    }

    public enum Operation
    {
        Equals = 0,
        GreaterThan = 1,
        LessThan = 2,
        GreaterThanOrEqual = 3,
        LessThanOrEqual = 4,
        Contains = 5,
        StartsWith = 6,
        EndsWith = 7,
        StringEquals = 8,
        NotEquals = 9
    }

}
