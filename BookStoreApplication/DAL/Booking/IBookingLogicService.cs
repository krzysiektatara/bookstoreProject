

using BookStoreApplicationAPI.Data.Entities;

namespace BookStoreApplicationAPI.Services.User
{
    public interface IBookingLogicService
    {
        bool isRequestetProductAvailable(int requested_qty, int available_qty);
        public bool isEntityDeleted(Entity entity);
    }
}
