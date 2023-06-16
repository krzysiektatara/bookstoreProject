using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplicationAPI.DAL.Services
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProduct(AddProductDto product);
        Task<Product> AddProductAsync(AddProductDto product);
        Task UpdateProductAsync(int id, AddProductDto? dto);
        Task DeleteProductAsync(int id);
        Task<Collection<ProductWithResource>> GetAllProductsWithResourceAsync();
        Task<IEnumerable<Product>> GetAllProducts();

    }
}
