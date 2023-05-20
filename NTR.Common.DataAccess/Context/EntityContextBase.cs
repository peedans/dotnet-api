using Microsoft.EntityFrameworkCore;

namespace NTR.Common.DataAccess.Context
{
    public class EntityContextBase<TContext> : DbContext, IEntityContext where TContext : DbContext
    {
        public EntityContextBase(DbContextOptions<TContext> options) : base(options)
        {
        }
    
    }
}
