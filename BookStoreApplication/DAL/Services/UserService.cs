using AutoMapper;
using BADataAccessLibrary.Models;
using BookStoreApplicationAPI.DAL.UOW;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;
using NuGet.Protocol.Plugins;

namespace BookStoreApplicationAPI.DAL.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            return _unitOfWork.Users.GetAsyncById(id);
        }

        public Task<User> GetUserByLoginAsync(string login)
        {
            return _unitOfWork.Users.GetAsync(x => x.Login == login);
        }
        public async Task<User> AddUserAsync(AddUserDto user)
        {
            var addedUser = await _unitOfWork.Users.AddAsync(_mapper.Map<User>(user));
            addedUser.RoleEnum = RoleEnum.Customer;

            _unitOfWork.SaveAsync();
            return addedUser;
        }

        public async Task UpdateUserAsync(int id, AddUserDto? dto)
        {
            var user = await _unitOfWork.Users.GetAsyncById(id);

            await _unitOfWork.Users.UpdateAsync(_mapper.Map(dto, user));

            _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
        }

        public async Task DeleteUserAsync(int id)
        {
            var userToDelete = await _unitOfWork.Users.GetAsyncById(id);
            await _unitOfWork.Users.DeleteAsync(userToDelete);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return users;
        }

        public async Task<bool> VerifyUserAndPassword(VMLogin modelLogin)
        {
            var user =await _unitOfWork.Users.GetAsync(x => x.Login == modelLogin.Login);
            return modelLogin.Password == user.Password;
        }
    }

}

