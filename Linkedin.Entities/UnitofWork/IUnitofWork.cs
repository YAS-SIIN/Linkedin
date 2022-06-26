using Linkedin.Entities.GenericRepository;
using System;

namespace Linkedin.Entities.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;

        void SaveChanges();

    }
}
