using Core.Repository;
using System;

namespace Infrastructure.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        int SubmitChanges();

        IRepository<T> Repository<T>() where T : class;
    }
}