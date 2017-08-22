using Core.PageResult;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Core.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);

        PagedListResult<TEntity> Search(SearchQuery<TEntity> searchQuery);

        int Count(Expression<Func<TEntity, bool>> filter = null);

        TEntity Get(object id);

        TEntity GetById(Expression<Func<TEntity, bool>> filter, string IncludeProperties = "");

        IEnumerable<TEntity> GetAll(string IncludeProperties = "");

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string IncludeProperties = "");

        void Add(TEntity entity);

        void Attach(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        bool Any(Expression<Func<TEntity, bool>> predicate);

        void Remove(object id);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entityToUpdate);
    }
}