﻿using System.Linq.Expressions;

namespace GameGoal.Data.GenericRepository
{
    public interface IDataRepository<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

        void Update(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}