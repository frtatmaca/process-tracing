using SakaryaBel.Core.DomainModel.Entities;
using SakaryaBel.Data.Repository;
using SakaryaBel.Data.UnitOfWork;
using SakaryaBel.Services.IService;
using System.Linq;

namespace SakaryaBel.Services.Service
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<Users> _userRepository;

        public RoleService(IUnitOfWork uow)
        {
            _uow = uow;
            _roleRepository = uow.GetRepository<Role>();
            _userRepository = uow.GetRepository<Users>();
        }

        /// <summary>
        /// Tüm roller.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }

        /// <summary>
        /// Kullanıcıya göre roller.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IQueryable<Role> GetRolesByUser(string userName)
        {
            return _userRepository.GetAll().FirstOrDefault(x => x.UserName == userName).Roles.AsQueryable();
        }

        /// <summary>
        /// Rol bul.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Role Find(int roleId)
        {
            return _roleRepository.Find(roleId);
        }

        /// <summary>
        /// Kullanıcı role sahip mi.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool IsUserInRole(string userName, string roleName)
        {
            return _userRepository.GetAll().FirstOrDefault(x => x.UserName == userName).Roles.Any(x => x.RoleName == roleName);
        }

        /// <summary>
        /// Kullanıcı ekle.
        /// </summary>
        /// <param name="role"></param>
        public void Insert(Role role)
        {
            _roleRepository.Insert(role);
        }

        /// <summary>
        /// Kullanıcı güncelle.
        /// </summary>
        /// <param name="role"></param>
        public void Update(Role role)
        {
            _roleRepository.Update(role);
        }

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="role">Rol</param>
        public void Delete(Role role)
        {
            _roleRepository.Delete(role);
        }

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="roleId">Rol Id</param>
        public void Delete(int roleId)
        {
            _roleRepository.Delete(roleId);
        }

        public IQueryable<Role> FindQuery(System.Linq.Expressions.Expression<System.Func<Role, bool>> predicate)
        {
            return _roleRepository.FindQuery(predicate);
        }

        public IQueryable<Role> GetAllEagerLoad(params System.Linq.Expressions.Expression<System.Func<Role, object>>[] children)
        {
            return _roleRepository.GetAllEagerLoad(children);
        }
    }
}
