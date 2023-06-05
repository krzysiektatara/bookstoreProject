using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;

namespace BookStoreApplicationAPI.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<UserAdressDto> GetUserWithAdressAsync(int id);
        Task<User> AddUserAsync(AddUserDto user);

    }
}
