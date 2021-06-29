using System;
using System.Linq;
using System.Linq.Expressions;

namespace SakaryaBel.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Tüm kayıtlar.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Kayıt bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(int id);

        /// <summary>
        /// Kayıt ekle.
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// Kayıt güncelle.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        /// Kayıt sil.
        /// </summary>
        /// <param name="id">Kayıt id</param>
        void Delete(int id);

        /// <summary>
        /// Kayıt sil.
        /// </summary>
        /// <param name="entityToDelete">Kayıt</param>
        void Delete(TEntity entityToDelete);

        IQueryable<TEntity> FindQuery(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAllEagerLoad(params Expression<Func<TEntity, object>>[] children);
    }
}
