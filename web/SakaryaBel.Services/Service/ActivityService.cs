using SakaryaBel.Core.DomainModel.Entities;
using SakaryaBel.Data.Repository;
using SakaryaBel.Data.UnitOfWork;
using SakaryaBel.Services.IService;
using System.Linq;

namespace SakaryaBel.Services.Service
{
    public class ActivityService : IActivityService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Activity> _activityRepository;

        public ActivityService(IUnitOfWork uow)
        {
            _uow = uow;      
            _activityRepository = uow.GetRepository<Activity>();
        }

        /// <summary>
        /// Tüm kullanıcılar.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Activity> GetAll()
        {
            return _activityRepository.GetAll();
        }        

        /// <summary>
        /// Kullanıcı bul.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Activity Find(int actId)
        {
            return _activityRepository.Find(actId);
        }

        /// <summary>
        /// Kullanıcı ekle.
        /// </summary>
        /// <param name="user"></param>
        public void Insert(Activity act)
        {
            _activityRepository.Insert(act);
        }

        /// <summary>
        /// Kullanıcı güncelle.
        /// </summary>
        /// <param name="user"></param>
        public void Update(Activity act)
        {
            _activityRepository.Update(act);
        }

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="user">Kullanıcı</param>
        public void Delete(Activity act)
        {
            _activityRepository.Delete(act);
        }

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="userId">Kullanıcı Id</param>
        public void Delete(int actId)
        {
            _activityRepository.Delete(actId);
        }


        public IQueryable<Activity> FindQuery(System.Linq.Expressions.Expression<System.Func<Activity, bool>> predicate)
        {
            return _activityRepository.FindQuery(predicate);
        }

        public IQueryable<Activity> GetAllEagerLoad(params System.Linq.Expressions.Expression<System.Func<Activity, object>>[] children)
        {
            return _activityRepository.GetAllEagerLoad(children);
        }
    }


}
