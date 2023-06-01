using BookStoreApplicationAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplicationAPI.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<ActionResult<T>> GetAsync(int id);

        Task<IEnumerable<T>> GetAll();

        Task<T> Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
