using BookStoreApplication.Models;
using BookStoreApplicationAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplicationAPI.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductEntity>
    {
        Task<ActionResult<ProductEntity>> Get(int id);

        Task<IEnumerable<Product>> GetProductsAsync();

        Task<ActionResult<Product>> GetProductAsync(int id);


        Task<ActionResult<ProductEntity>> GetProductByNameAsync(string name);

        Task<ProductEntity> AddProductAsync(AddProductDto product);

        //Task Delete(int id);

    }
}
