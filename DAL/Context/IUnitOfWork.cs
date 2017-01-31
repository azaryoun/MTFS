using MTFS.Business.Domain.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MTFS.DAL.Context
{
    public interface IUnitOfWork
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : Base;
        int SaveChanges(); 
        Task<int> SaveChangesAsync();
    }
}
