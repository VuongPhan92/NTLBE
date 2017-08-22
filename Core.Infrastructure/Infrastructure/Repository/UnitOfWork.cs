using Core.Repository;
using System;
using System.Collections;
using System.Data.Entity;

namespace Infrastructure.Repository
{
    public class UnitOfWork<TContext> : IUnitOfWork, IDisposable where TContext : DbContext, new()
    {
        private readonly DbContext _context;

        /// <summary>
        /// NOTE: repository getters instantiate repositories as needed (lazily)...
        ///       i wish I knew of IoC "way" of wiring up repository getters...
        /// </summary>
        /// <param name="dbContextFactory"></param>
        public UnitOfWork()
        {
            _context = new TContext();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SubmitChanges()
        {
            return _context.SaveChanges();
        }

        private Hashtable _repositories;

        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type];
        }
    }
}