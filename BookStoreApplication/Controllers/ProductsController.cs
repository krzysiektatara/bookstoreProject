
using Microsoft.AspNetCore.Mvc;
using BookStoreApplicationAPI.DAL.UOW;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Models;
using BookStoreApplicationAPI.Data.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BookStoreApplicationAPI.Services.User;
using BookStoreApplicationAPI.Data.Dto;

namespace BookStoreApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookingLogicService _bookingLogicService;

        public ProductsController(
            IBookingLogicService bookingLogicService,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bookingLogicService = bookingLogicService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("{productId}", Name = nameof(GetProduct))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            var product = await _unitOfWork.Products.GetAsyncById(productId);
            return Ok(product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            var newProduct = await _unitOfWork.Products.AddAsync(product);
            _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();

            return CreatedAtAction(nameof(GetProduct), new { productId = newProduct.Id }, newProduct);
        }

        /// <summary>
        /// get list of all products with url
        /// </summary>
        /// <returns></returns>
        ///  <response code="404">If list of products is null</response>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete(Name = nameof(DeleteProduct))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> DeleteProduct(int Id)
        {
            var productToDelete = await _unitOfWork.Products.GetAsyncById(Id);

            await _unitOfWork.Products.RemoveAsync(productToDelete);
            if (_bookingLogicService.isEntityDeleted(productToDelete))
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
        /// <param name="productId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPatch("{productId}", Name = nameof(EditProduct))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> EditProduct(
            int productId, [FromBody] AddProductDto product)
        {
            var productToEdit = await _unitOfWork.Products.GetAsyncById(productId);

            await _unitOfWork.Products.UpdateAsync(productToEdit, product);
            _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
            return Ok();
        }
    }
}
