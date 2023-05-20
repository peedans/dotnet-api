using System;
namespace NTR.Common.DataAccess.Entities
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class UowLogInfo : Attribute
    {
        public UowLogInfo()
        {
        }
    }
}

