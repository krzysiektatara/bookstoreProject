using BADataAccessLibrary.Models;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;

namespace BookStoreApplicationAPI.DAL.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByLoginAsync(string login);
        Task<bool> VerifyUserAndPassword(VMLogin logmodelLoginin);
        Task<User> AddUserAsync(AddUserDto user);
        Task UpdateUserAsync(int id, AddUserDto? dto);
        Task DeleteUserAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();

    }
}
