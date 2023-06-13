using Microsoft.AspNetCore.Mvc;
using BookStoreApplicationAPI.DAL.UOW;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Services.User;
using BookStoreApplicationAPI.Data.Dto;

namespace BookStoreApplication.Controllers
{
    /// <summary>
    /// UsersController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookingLogicService _bookingLogicService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingLogicService"></param>
        /// <param name="unitOfWork"></param>
        public UsersController(
            IBookingLogicService bookingLogicService,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bookingLogicService = bookingLogicService;
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
            var user = await _unitOfWork.Users.GetAsyncById(userId);

            return Ok(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            var newUser = await _unitOfWork.Users.AddAsync(user);
            _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
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
            var users = await _unitOfWork.Users.GetAll();
            return users;
        }

        [HttpDelete(Name = nameof(DeleteUser))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> DeleteUser(int Id)
        {
            var userToDelete = await _unitOfWork.Users.GetAsyncById(Id);

            await _unitOfWork.Users.RemoveAsync(userToDelete);
            if (_bookingLogicService.isEntityDeleted(userToDelete))
            {
                _unitOfWork.SaveAsync();
                _unitOfWork.Dispose();
                return Ok();
            }
            return BadRequest();
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
            var userToEdit = await _unitOfWork.Users.GetAsyncById(userId);
         
            await _unitOfWork.Users.UpdateAsync(userToEdit, user);
                _unitOfWork.SaveAsync();
                _unitOfWork.Dispose();
                return Ok();
        }
    }
}
