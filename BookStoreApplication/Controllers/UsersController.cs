using Microsoft.AspNetCore.Mvc;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.DAL.Services;

namespace BookStoreApplication.Controllers
{
    /// <summary>
    /// UsersController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingLogicService"></param>
        /// <param name="unitOfWork"></param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}", Name = nameof(GetUser))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            return Ok(user);
        }

        /// <summary>
        /// Create new user with customer Role.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<User>> CreateNewUser(AddUserDto user)
        {
            var newUser = await _userService.AddUserAsync(user);

            return CreatedAtAction(nameof(GetUser), new { userId = newUser.Id }, newUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetUsers))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return users;
        }

        [HttpDelete(Name = nameof(DeleteUser))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> DeleteUser(int Id)
        {
            await _userService.DeleteUserAsync(Id);

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPatch("{userId}", Name = nameof(EditUser))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> EditUser(
            int userId, [FromBody] AddUserDto user)
        {
            await _userService.UpdateUserAsync(userId, user);

            return Ok();
        }
    }
}
