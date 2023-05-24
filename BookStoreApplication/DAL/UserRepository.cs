using AutoMapper;
using BookStoreApplication.Models;
using BookStoreApplicationAPI.Models;
using BookStoreApplicationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplicationAPI.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(
            BookStoreDbContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserEntity> GetUserAsync(int id)
        {

            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<UserAdressDto> GetUserWithAdressAsync(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            return new UserAdressDto
            {
                User_Id = user.Id,
                Address = user.Address
            };
        }

        public async Task<UserEntity> AddUserAsync(AddUserDto user)
        {
            var a = _context.Users.Add(_mapper.Map<UserEntity>(user));

            return a.Entity;
        }
    }
}
