using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApplication.Models;
using BookStoreApplicationAPI.Data;
using BookStoreApplicationAPI.Controllers;
using System.Data;
using BookStoreApplicationAPI.Services.User;
using BookStoreApplicationAPI.Models;
using BookStoreApplicationAPI.Repositories;
using BookStoreApplicationAPI.Repositories.UOW;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<UserEntity>> Add(AddUserDto user)
        {
            if (_unitOfWork.Users.GetUserAsync == null)
            {
                return Problem("Entity set 'UserContext.Users'  is null.");
            }
            var  newUser = await _unitOfWork.Users.AddUserAsync(user);
            _unitOfWork.Save();

            return Created(string.Empty, newUser);
        }

        //        //GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<UserEntity>> GetUser(int id)
        {
            var user = await _unitOfWork.Users.GetUserAsync(id);
            if (user == null) return NotFound();

            return user;
        }

    }
}
