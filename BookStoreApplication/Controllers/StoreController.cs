using Microsoft.AspNetCore.Mvc;
using BookStoreApplicationAPI.Models;
using BookStoreApplicationAPI.Repositories.UOW;

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
        public async Task<ActionResult<StoreItemEntity>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Store.GetByProductIdAsync(id);
            if (product == null) return NotFound();

            return product;
        }

        [HttpPatch("{id}/{quantity}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(201)]
        public async Task<ActionResult<StoreItemEntity>> UpdateProduct(int id, int quantity)
        {
            var product = await _unitOfWork.Store.GetByProductIdAsync(id);
            if (product == null) return NotFound();
            var updatedProduct = _unitOfWork.Store.Update(id, quantity);
            if (updatedProduct == null) return NotFound();
            else
            {
                _unitOfWork.Save();
                return Created(string.Empty, product);
            }
        }
    }
}

