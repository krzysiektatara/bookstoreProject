
using AutoMapper;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;
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

        public async Task<StoreItem?> Update(int id, int qty)
        {
            var item = await _context.Book_store.SingleOrDefaultAsync(x => x.ProductId == id);
            if (item == null) return null;
            item.Available_qty += qty;
            //_context.Entry(item).State = EntityState.Unchanged;
            //item.Available_qty += qty;
            //_context.SaveChanges();
            return item;
        }


        public async Task<StoreItem?> UpdateProductQuantity(ItemQuantityDto itemRequest)
        {
            var item = await _context.Book_store.SingleOrDefaultAsync(x => x.ProductId == itemRequest.Product_Id);

            item.Available_qty -= itemRequest.Requested_qty;
            item.Booked_qty += itemRequest.Requested_qty;
            //int isChange = await _context.SaveChangesAsync();
            //if (isChange > 0) return item.Available_qty - itemRequest.Requested_qty;
            //return null;         
            return _context.Book_store.Update(item).Entity;
        }

    }
}

