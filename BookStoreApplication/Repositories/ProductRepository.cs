using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApplication.Models;
using BookStoreApplicationAPI.Controllers;
using BookStoreApplicationAPI.Data;
using BookStoreApplicationAPI.Models;
using BookStoreApplicationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplicationAPI.Repositories
{
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly AutoMapper.IConfigurationProvider _mappingConfiguration;
        public ProductRepository(
            BookStoreDbContext context,
            IMapper mapper,
            AutoMapper.IConfigurationProvider mappingConfiguration
            )
        {
            _context = context;
            _mapper = mapper;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<ProductEntity> AddProductAsync(AddProductDto product)
        {
            var a = _context.Products.Add(_mapper.Map<ProductEntity>(product));
            //var isSucess = await _context.SaveChangesAsync();
            //if (isSucess < 1)
            //{
            //    throw new Exception("product not added");
            //}
            return a.Entity;
        }

        public async Task<ActionResult<ProductEntity>> Geta(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);

            return product;
        }


        public async Task<ActionResult<Product>> GetProductAsync(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);

            Link link = Link.To(
                nameof(ProductsController.GetProduct),
                new { productId = product.Id }
                );
            var p = new Product
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

        public async Task<ActionResult<ProductEntity>> GetProductByNameAsync(string name)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Name == name);

            if (product == null)
            {
                return null;
            }
            var p = new ProductEntity
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


        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var querry = _context.Products.
                ProjectTo<Product>(_mappingConfiguration);

            return await querry.ToArrayAsync();
        }

        public async Task Delete(ProductEntity entity)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == entity.Id);
            if (product == null) return;

            _context.Products.Remove(product);
            //await _context.SaveChangesAsync();
        }
    }
}
