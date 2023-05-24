using BookStoreApplication.Models;
using BookStoreApplicationAPI.Models;

namespace BookStoreApplicationAPI.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        Task<StoreItemEntity> GetByProductIdAsync(int id);
        Task<StoreItemEntity?> UpdateProductQuantity(ItemQuantityDto itemRequest);
        Task<StoreItemEntity?> Update(int id, int qty);

        //Task VerifyProductAvailabilty(ItemQuantityDto itemRequest);
    }
}
