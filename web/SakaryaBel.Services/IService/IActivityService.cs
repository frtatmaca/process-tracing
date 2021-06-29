using SakaryaBel.Core.DomainModel.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SakaryaBel.Services.IService
{
    public interface IActivityService
    {
        /// <summary>
        /// Tüm kullanıcılar.
        /// </summary>
        /// <returns></returns>
        IQueryable<Activity> GetAll();

        /// <summary>
        /// Role göre kullanıcılar.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        //IQueryable<Activity> GetUsersByRole(string roleName);

        /// <summary>
        /// Kullanıcı sistemde kayıtlı mı.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //bool ValidateUser(string userName, string password);

        /// <summary>
        /// Kullanıcı bul.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Activity Find(int actId);

        /// <summary>
        /// Kullanıcı ekle.
        /// </summary>
        /// <param name="user"></param>
        void Insert(Activity act);

        /// <summary>
        /// Kullanıcı güncelle.
        /// </summary>
        /// <param name="user"></param>
        void Update(Activity act);

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="user">Kullanıcı</param>
        void Delete(Activity act);

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="userId">Kullanıcı Id</param>
        void Delete(int act);

        IQueryable<Activity> FindQuery(Expression<Func<Activity, bool>> predicate);

        IQueryable<Activity> GetAllEagerLoad(params Expression<Func<Activity, object>>[] children);
    }
}
