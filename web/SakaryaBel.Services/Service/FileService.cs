using SakaryaBel.Core.DomainModel.Entities;
using SakaryaBel.Data.Repository;
using SakaryaBel.Data.UnitOfWork;
using SakaryaBel.Services.IService;
using System.Linq;

namespace SakaryaBel.Services.Service
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<File> _fileRepository;

        public FileService(IUnitOfWork uow)
        {
            _uow = uow;
            _fileRepository = uow.GetRepository<File>();
        }

        /// <summary>
        /// Tüm kullanıcılar.
        /// </summary>
        /// <returns></returns>
        public IQueryable<File> GetAll()
        {
            return _fileRepository.GetAll();
        }        

        /// <summary>
        /// Kullanıcı bul.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public File Find(int refId)
        {
            return _fileRepository.Find(refId);
        }


        /// <summary>
        /// Kullanıcı ekle.
        /// </summary>
        /// <param name="user"></param>
        public void Insert(File reference)
        {
            _fileRepository.Insert(reference);
        }

        /// <summary>
        /// Kullanıcı güncelle.
        /// </summary>
        /// <param name="user"></param>
        public void Update(File reference)
        {
            _fileRepository.Update(reference);
        }

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="user">Kullanıcı</param>
        public void Delete(File reference)
        {
            _fileRepository.Delete(reference);
        }

        /// <summary>
        /// Kullanıcı sil.
        /// </summary>
        /// <param name="userId">Kullanıcı Id</param>
        public void Delete(int refId)
        {
            _fileRepository.Delete(refId);
        }


        public IQueryable<File> FindQuery(System.Linq.Expressions.Expression<System.Func<File, bool>> predicate)
        {
            return _fileRepository.FindQuery(predicate);
        }

        public IQueryable<File> GetAllEagerLoad(params System.Linq.Expressions.Expression<System.Func<File, object>>[] children)
        {
            return _fileRepository.GetAllEagerLoad(children);
        }
    }


}
