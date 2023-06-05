using Microsoft.AspNetCore.Mvc;
using BookStoreApplicationAPI.DAL.UOW;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Dto;

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
        public async Task<ActionResult<User>> Add(User user)
        {
            if (_unitOfWork.Users.GetAsync == null)
            {
                return Problem("Entity set 'UserContext.Users'  is null.");
            }
            var  newUser = await _unitOfWork.Users.Add(user);
            _unitOfWork.Save();

            return Created(string.Empty, newUser);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("/2")]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<User>> Add2(User user)
        {
            if (_unitOfWork.Users.GetAsync == null)
            {
                return Problem("Entity set 'UserContext.Users'  is null.");
            }
            var newUser = await _unitOfWork.Users.Add(user);
            _unitOfWork.Save();

            return Created(string.Empty, newUser);
        }

        //        //GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _unitOfWork.Users.GetAsync(id);
            if (user == null) return NotFound();

            return user;
        }

    }
}
