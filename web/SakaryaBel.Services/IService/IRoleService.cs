using SakaryaBel.Core.DomainModel.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SakaryaBel.Services.IService
{
    public interface IRoleService
    {
        /// <summary>
        /// Tüm roller.
        /// </summary>
        /// <returns></returns>
        IQueryable<Role> GetAll();

        /// <summary>
        /// Kullanıcıya göre roller.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IQueryable<Role> GetRolesByUser(string userName);

        /// <summary>
        /// Rol bul.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Role Find(int roleId);

        /// <summary>
        /// Kullanıcı role sahip mi.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        bool IsUserInRole(string userName, string roleName);

        /// <summary>
        /// Kullanıcı ekle.
        /// </summary>
        /// <param name="role"></param>
        void Insert(Role role);

        /// <summary>
        /// Kullanıcı güncelle.
        /// </summary>
        /// <param name="role"></param>
        void Update(Role role);

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="role">Rol</param>
        void Delete(Role role);

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="roleId">Rol Id</param>
        void Delete(int roleId);

        IQueryable<Role> FindQuery(Expression<Func<Role, bool>> predicate);

        IQueryable<Role> GetAllEagerLoad(params Expression<Func<Role, object>>[] children);
    }
}
