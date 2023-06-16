using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Models;

namespace BookStoreApplicationAPI.Repositories.Interfaces
{
    public interface IStoreRepository : IGenericRepository<StoreItem>
    {
        Task<StoreItem?> UpdateProductQuantity(ItemQuantityDto itemRequest);
        Task<StoreItem?> Update(int id, int qty);

        //Task VerifyProductAvailabilty(ItemQuantityDto itemRequest);
    }
}
