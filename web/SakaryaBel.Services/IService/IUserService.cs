using SakaryaBel.Core.DomainModel.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SakaryaBel.Services.IService
{
    public interface IUserService
    {
        /// <summary>
        /// Tüm kullanıcılar.
        /// </summary>
        /// <returns></returns>
        IQueryable<Users> GetAll();

        /// <summary>
        /// Role göre kullanıcılar.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IQueryable<Users> GetUsersByRole(string roleName);

        /// <summary>
        /// Kullanıcı sistemde kayıtlı mı.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool ValidateUser(string userName, string password);

        /// <summary>
        /// Kullanıcı bul.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Users Find(int userId);

        /// <summary>
        /// Kullanıcı ekle.
        /// </summary>
        /// <param name="user"></param>
        void Insert(Users user);

        /// <summary>
        /// Kullanıcı güncelle.
        /// </summary>
        /// <param name="user"></param>
        void Update(Users user);

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="user">Kullanıcı</param>
        void Delete(Users user);

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="userId">Kullanıcı Id</param>
        void Delete(int userId);

        IQueryable<Users> FindQuery(Expression<Func<Users, bool>> predicate);

        IQueryable<Users> GetAllEagerLoad(params Expression<Func<Users, object>>[] children);
    }
}
