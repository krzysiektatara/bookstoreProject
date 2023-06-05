using AutoMapper;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplicationAPI.DAL
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BookStoreDbContext context, IMapper mapper, AutoMapper.IConfigurationProvider mappingConfiguration) 
            : base(context, mapper, mappingConfiguration)
        {
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

        public async Task<User> AddUserAsync(AddUserDto user)
        {
            var a = _context.Users.Add(_mapper.Map<User>(user));

            return a.Entity;
        }
    }
}
