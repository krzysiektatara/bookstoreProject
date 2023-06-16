using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplicationAPI.Repositories.Interfaces
{
    public interface IProductRepository<ProductWithResource> : IGenericRepository<Product>
    {
        Task<IEnumerable<ProductWithResource>> GetAllWithResourceAsync();

    }
}
