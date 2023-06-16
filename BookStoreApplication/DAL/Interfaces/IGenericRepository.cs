using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BookStoreApplicationAPI.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class, IEntityBase, new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsyncById(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> Add(T entity);
        Task<T> AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task DeleteAsync(T entity);
        Task Update(T entity, Object? dto = null);
        Task UpdateAsync(T entity);
    }
}
