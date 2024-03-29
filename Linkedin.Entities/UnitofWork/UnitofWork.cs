﻿using Linkedin.Entities.Context;
using Linkedin.Entities.GenericRepository;
using System;
namespace Linkedin.Entities.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDataBase _context;
        private bool disposed = false;

        public UnitOfWork(MyDataBase context)
        {

            //Database.SetInitializer<MyDataBase>(null);
            if (context == null)
                throw new ArgumentException("context is null!");
            _context = context;
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }


        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch
            {
            }
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
