using BookStoreApplication.Models;
using BookStoreApplicationAPI.Data;
using BookStoreApplicationAPI.Exceptions;
using BookStoreApplicationAPI.Models;
using BookStoreApplicationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace BookStoreApplicationAPI.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly BookStoreDbContext _context;
        public StoreRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<StoreItemEntity> GetByProductIdAsync(int productId)
        {
            var item = await _context.Book_store.SingleOrDefaultAsync(x => x.Product_Id == productId);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        public async Task<StoreItemEntity?> Update(int id, int qty)
        {
            var item = await _context.Book_store.SingleOrDefaultAsync(x => x.Product_Id == id);
            if (item == null) return null;
            item.Available_qty += qty;
            //_context.Entry(item).State = EntityState.Unchanged;
            //item.Available_qty += qty;
            //_context.SaveChanges();
            return item;
        }

        public async Task<StoreItemEntity?> UpdateProductQuantity(ItemQuantityDto itemRequest)
        {
            var item = await _context.Book_store.SingleOrDefaultAsync(x => x.Product_Id == itemRequest.Product_Id);

            item.Available_qty -= itemRequest.Requested_qty;
            item.Booked_qty += itemRequest.Requested_qty;
            //int isChange = await _context.SaveChangesAsync();
            //if (isChange > 0) return item.Available_qty - itemRequest.Requested_qty;
            //return null;         
            return _context.Book_store.Update(item).Entity;
        }

    }
}

