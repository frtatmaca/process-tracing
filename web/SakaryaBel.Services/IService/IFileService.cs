using SakaryaBel.Core.DomainModel.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SakaryaBel.Services.IService
{
    public interface IFileService
    {
        /// <summary>
        /// Tüm kullanıcılar.
        /// </summary>
        /// <returns></returns>
        IQueryable<File> GetAll();


        /// <summary>
        /// Kullanıcı bul.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        File Find(int fileId);

        /// <summary>
        /// Kullanıcı ekle.
        /// </summary>
        /// <param name="user"></param>
        void Insert(File file);

        /// <summary>
        /// Kullanıcı güncelle.
        /// </summary>
        /// <param name="user"></param>
        void Update(File file);

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="user">Kullanıcı</param>
        void Delete(File file);

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="userId">Kullanıcı Id</param>
        void Delete(int fileId);

        IQueryable<File> FindQuery(Expression<Func<File, bool>> predicate);

        IQueryable<File> GetAllEagerLoad(params Expression<Func<File, object>>[] children);
    }
}
