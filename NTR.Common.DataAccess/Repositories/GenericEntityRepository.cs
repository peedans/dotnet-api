using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NTR.Common.DataAccess.Entities;

namespace NTR.Common.DataAccess.Repositories
{
    public class GenericEntityRepository<TEntity> : EntityRepositoryBase<DbContext, TEntity> where TEntity : EntityBase, new()
    {
		public GenericEntityRepository(ILogger<DataAccess> logger) : base(logger, null)
		{ }
	}
}