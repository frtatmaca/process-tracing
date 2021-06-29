using SakaryaBel.Data.Repository;
using System;

namespace SakaryaBel.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
       // IUserRepository Users { get; }

        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}
