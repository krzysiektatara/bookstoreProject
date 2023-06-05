using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApplicationAPI.Controllers;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Models;
using BookStoreApplicationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplicationAPI.DAL
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        public ProductRepository(BookStoreDbContext context, IMapper mapper, AutoMapper.IConfigurationProvider mappingConfiguration) 
            : base(context, mapper, mappingConfiguration)
        {
        }

        public async Task<ActionResult<ProductWithResource>> GetProductAsync(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);

            Link link = Link.To(
                nameof(ProductsController.GetProduct),
                new { productId = product.Id }
                );
            var p = new ProductWithResource
            {
                Self = link,
                Id = product.Id,
                Author = product.Author,
                Description = product.Description,
                Image_Path = product.Image_Path,
                Name = product.Name,
                Price = product.Price
            };

            return p;
        }

        public async Task<ActionResult<Product>> GetProductByNameAsync(string name)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Name == name);

            if (product == null)
            {
                return null;
            }
            var p = new Product
            {
                Id = product.Id,
                Author = product.Author,
                Description = product.Description,
                Image_Path = product.Image_Path,
                Name = product.Name,
                Price = product.Price
            };

            return p;
        }


        public async Task<IEnumerable<ProductWithResource>> GetProductsAsync()
        {
            var querry = _context.Products.
                ProjectTo<ProductWithResource>(_mappingConfiguration);

            return await querry.ToArrayAsync();
        }
    }
}
