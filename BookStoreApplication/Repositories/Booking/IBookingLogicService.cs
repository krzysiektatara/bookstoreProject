using BookStoreApplication.Models;
using BookStoreApplicationAPI.Models;

namespace BookStoreApplicationAPI.Services.User
{
    public interface IBookingLogicService
    {
        bool isRequestetProductAvailable(int requested_qty, int available_qty);
    }
}
