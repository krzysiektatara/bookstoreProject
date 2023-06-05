
using Microsoft.AspNetCore.Mvc;
using BookStoreApplicationAPI.DAL.UOW;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Models;
using BookStoreApplicationAPI.Data.Exceptions;

namespace BookStoreApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(
             IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            if (_unitOfWork.Products.GetProductByNameAsync(product.Name) == null)
            {
                return BadRequest(new ApiError(
                   $"product with name {product.Name}, already exists.")
                );
            }

            var newProduct = await _unitOfWork.Products.Add(product);
            _unitOfWork.Save();
            _unitOfWork.Dispose();

            return Created(string.Empty, newProduct);
        }

        [HttpGet("{productId}", Name = nameof(GetProduct))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            var product = await _unitOfWork.Products.GetAsync(productId);
            if (product.Value == null) return NotFound();
            return product;
        }

        /// <summary>
        /// get list of all products
        /// </summary>
        /// <returns></returns>
        ///  <response code="200">If list of products is null</response>
        [HttpGet(Name = nameof(GetProducts))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<ProductWithResource>>> GetProducts()
        {
            var products = await _unitOfWork.Products.GetProductsAsync();

            var collection = new Collection<ProductWithResource>()
            {
                Self = Link.ToCollection(nameof(GetProducts)),
                Value = products.ToArray(),

            };

            return collection;
        }

        //DELETE: api/Product/5
        [HttpDelete("{bookingId}", Name = nameof(DeleteProductById))]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var product = _unitOfWork.Products.GetAsync(id).Result.Value;
            if (product == null) return NotFound();
            _unitOfWork.Products.Delete(product);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
