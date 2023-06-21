
using AutoMapper;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Exceptions;
using BookStoreApplicationAPI.Data.Models;
using BookStoreApplicationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplicationAPI.DAL
{
    public class StoreRepository : GenericRepository<StoreItem>, IStoreRepository
    {
        private readonly BookStoreDbContext _context;

        public StoreRepository(BookStoreDbContext context, AutoMapper.IConfigurationProvider mappingConfiguration)
            : base(context, mappingConfiguration)
        {
        }

        public override async Task UpdateAsync(StoreItem entity)
        {
            if (entity.Available_qty < 0)
            {
                var availableQuantity = await _context.Book_store.SingleOrDefaultAsync(x => x.ProductId == entity.Id);
                throw new RequestedItemIsUnavailableException($"Requested Quantity is not available, available amount is {availableQuantity.Available_qty}");
            }
            await base.UpdateAsync(entity);
        }
    }
}

