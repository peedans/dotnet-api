using System;
using System.Collections.Generic;
using System.Text;

namespace NTR.Common.DataAccess.Extensions
{
    public static class NumericExtention
    {
        public static bool IsNumber(this object value)
        {
            #region coding
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
            #endregion
        }

    }
}
