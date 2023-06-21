using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplicationAPI.DAL.Services
{
    public interface IStoreService
    {
        Task<StoreItem> GetStoreItemByIdAsync(int id);
        public Task<StoreItem> GetStoreItemByProductIdAsync(int productId);
        Task<StoreItem> AddStoreItem(AddStoreItemDto product);
        Task<StoreItem> AddStoreItemAsync(AddStoreItemDto product);
        Task UpdateStoreItemAsync(int id, int quantity);

        Task RequestStoreItemAsync(int id, int requested_qty);
        Task DeleteStoreItemAsync(int id);
        Task<IEnumerable<StoreItem>> GetAllStoreItems();
        Task<Booking> GetBookingByIdAsync(int id);
        Task<Booking> CreateBookingAsync(BookingRequestDto product, int UserId);

    }
}
