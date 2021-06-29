using SakaryaBel.Core.DomainModel.Entities;
using SakaryaBel.Data.Repository;
using SakaryaBel.Data.UnitOfWork;
using SakaryaBel.Services.IService;
using System.Linq;

namespace SakaryaBel.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<Users> _userRepository;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
            _roleRepository = uow.GetRepository<Role>();
            _userRepository = uow.GetRepository<Users>();
        }

        /// <summary>
        /// Tüm kullanıcılar.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Users> GetAll()
        {
            return _userRepository.GetAll();
        }

        /// <summary>
        /// Role göre kullanıcılar.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IQueryable<Users> GetUsersByRole(string roleName)
        {
            return _roleRepository.GetAll().FirstOrDefault(x => x.RoleName == roleName).Users.AsQueryable();
        }

        /// <summary>
        /// Kullanıcı sistemde kayıtlı mı.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateUser(string userName, string password)
        {
            return _userRepository.GetAll().Any(x => x.UserName == userName && x.Password == password);
        }

        /// <summary>
        /// Kullanıcı bul.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Users Find(int userId)
        {
            return _userRepository.Find(userId);
        }

        /// <summary>
        /// Kullanıcı ekle.
        /// </summary>
        /// <param name="user"></param>
        public void Insert(Users user)
        {
            _userRepository.Insert(user);
        }

        /// <summary>
        /// Kullanıcı güncelle.
        /// </summary>
        /// <param name="user"></param>
        public void Update(Users user)
        {
            _userRepository.Update(user);
        }

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="user">Kullanıcı</param>
        public void Delete(Users user)
        {
            _userRepository.Delete(user);
        }

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="userId">Kullanıcı Id</param>
        public void Delete(int userId)
        {
            _userRepository.Delete(userId);
        }


        public IQueryable<Users> FindQuery(System.Linq.Expressions.Expression<System.Func<Users, bool>> predicate)
        {
            return _userRepository.FindQuery(predicate);
        }

        public IQueryable<Users> GetAllEagerLoad(params System.Linq.Expressions.Expression<System.Func<Users, object>>[] children)
        {
            return _userRepository.GetAllEagerLoad(children);
        }
    }
}
