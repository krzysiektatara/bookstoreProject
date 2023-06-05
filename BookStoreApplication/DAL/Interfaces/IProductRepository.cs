using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplicationAPI.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {

        Task<IEnumerable<ProductWithResource>> GetProductsAsync();

        Task<ActionResult<ProductWithResource>> GetProductAsync(int id);

        Task<ActionResult<Product>> GetProductByNameAsync(string name);


    }
}
