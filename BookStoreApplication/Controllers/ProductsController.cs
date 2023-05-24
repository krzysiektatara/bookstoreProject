
using Microsoft.AspNetCore.Mvc;
using BookStoreApplicationAPI.Exceptions;
using BookStoreApplicationAPI.Models;
using BookStoreApplicationAPI.DAL.UOW;

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
        public async Task<ActionResult<ProductEntity>> AddProduct(AddProductDto product)
        {
            if (_unitOfWork.Products.GetProductByNameAsync(product.Name) == null)
            {
                return BadRequest(new ApiError(
                   $"product with name {product.Name}, already exists.")
                   );
            }
            var newProduct = await _unitOfWork.Products.AddProductAsync(product);
            _unitOfWork.Save();

            return Created(string.Empty, newProduct);
        }

        [HttpGet("{productId}", Name = nameof(GetProduct))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            var product = await _unitOfWork.Products.GetProductAsync(productId);
            if (product == null) return NotFound();
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
        public async Task<ActionResult<Collection<Product>>> GetProducts()
        {
            var products = await _unitOfWork.Products.GetProductsAsync();

            var collection = new Collection<Product>()
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
            await _unitOfWork.Products.Delete(id);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
