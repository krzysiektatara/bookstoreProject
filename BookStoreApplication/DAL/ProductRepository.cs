using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplicationAPI.DAL
{
    public class ProductRepository<ProductWithResource> : GenericRepository<Product>, IProductRepository<ProductWithResource>
    {

        public ProductRepository(BookStoreDbContext context, AutoMapper.IConfigurationProvider mappingConfiguration) : base(context, mappingConfiguration)
        {
        }

        public async Task<IEnumerable<ProductWithResource>> GetAllWithResourceAsync()
        {
            var querry = _context.Products.
                ProjectTo<ProductWithResource>(_mappingConfiguration);

            return await querry.ToArrayAsync();
        }
    }
}
