using Microsoft.AspNetCore.Mvc;
using BookStoreApplicationAPI.DAL.UOW;
using BookStoreApplicationAPI.Data.Entities;

namespace BookStoreApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreController(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<StoreItem>> GetStoreItemByIdAsync(int id)
        {
            var product = await _unitOfWork.Store.GetAsyncById(id);
            if (product == null) return NotFound();

            return product;
        }

        [HttpPatch("{id}/{quantity}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(201)]
        public async Task<ActionResult<StoreItem>> UpdateProduct(int id, int quantity)
        {
            var product = await _unitOfWork.Store.GetAsyncById(id);
            if (product == null) return NotFound();
            var updatedProduct = _unitOfWork.Store.Update(id, quantity);
            if (updatedProduct == null) return NotFound();
            else
            {
                _unitOfWork.SaveAsync();
                return Created(string.Empty, product);
            }
        }
    }
}

