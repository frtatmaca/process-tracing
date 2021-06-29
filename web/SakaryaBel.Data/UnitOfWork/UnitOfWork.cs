
using SakaryaBel.Data.Context;
using SakaryaBel.Data.Repository;
using SakaryaBel.Data.UnitOfWork;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace EducationProject.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
       // public IUserRepository Users { get { var repo = GetRepository<User>(); repo.Uow = this; return repo; } }


        private readonly MvcProjectContext _context;

        public UnitOfWork(MvcProjectContext context)
        {
            Database.SetInitializer<MvcProjectContext>(null);

            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_context);
        }

        public int SaveChanges()
        {
            try
            {
                if (_context == null)
                    throw new ArgumentNullException("_context");

                return _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);

                throw fail;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
