using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkedin.Entities.GenericRepository;
using Student.Data.Context;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Linkedin.Entities.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDataBase _context;
        private DB trans = null;
        private bool disposed = false;

        public UnitOfWork(MyDataBase context)
        {
            
            Database.SetInitializer<MyDataBase>(null);
            if (context == null)
                throw new ArgumentException("context is null!");
            _context = context;
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }

        public void BeginTransaction()
        {
            trans = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            trans.Commit();
        }

        public void Rollback()
        {
            trans.Rollback();
        }

        public int SaveChanges()
        {
            int affectedRow = 0;
            try
            {
                affectedRow = _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var message = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        message = string.Format("Property: {0}, Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(message, dbEx);
            }
            return affectedRow;
        }
                
        public virtual void Dispose(bool disposing)
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
