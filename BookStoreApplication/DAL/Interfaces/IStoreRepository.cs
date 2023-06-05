using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;

namespace BookStoreApplicationAPI.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        Task<StoreItem> GetByProductIdAsync(int id);
        Task<StoreItem?> UpdateProductQuantity(ItemQuantityDto itemRequest);
        Task<StoreItem?> Update(int id, int qty);

        //Task VerifyProductAvailabilty(ItemQuantityDto itemRequest);
    }
}
