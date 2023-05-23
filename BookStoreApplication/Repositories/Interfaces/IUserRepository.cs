using BookStoreApplication.Models;
using BookStoreApplicationAPI.Models;

namespace BookStoreApplicationAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserAsync(int id);
        Task<UserAdressDto> GetUserWithAdressAsync(int id);
        Task<UserEntity> AddUserAsync(AddUserDto user);

    }
}
