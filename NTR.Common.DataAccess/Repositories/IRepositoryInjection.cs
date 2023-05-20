using Microsoft.EntityFrameworkCore;

namespace NTR.Common.DataAccess.Repositories
{
    public interface IRepositoryInjection
    {
        IRepositoryInjection SetContext(DbContext context);
    }
}