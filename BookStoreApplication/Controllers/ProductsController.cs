﻿
using Microsoft.AspNetCore.Mvc;
using BookStoreApplicationAPI.DAL.UOW;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Models;
using BookStoreApplicationAPI.Data.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BookStoreApplicationAPI.Services.User;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.DAL.Services;

namespace BookStoreApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookingLogicService _bookingLogicService;
        private readonly IProductService _productService;

        public ProductsController(
            IBookingLogicService bookingLogicService,
            IUnitOfWork unitOfWork,
            IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _bookingLogicService = bookingLogicService;
            _productService = productService;
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
            var product = await _productService.GetProductByIdAsync(productId);
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
        public async Task<ActionResult<Product>> AddProduct(AddProductDto product)
        {
            var newProduct = await _productService.AddProductAsync(product);
 

            return CreatedAtAction(nameof(GetProduct), new { productId = newProduct.Id }, newProduct);
        }

        /// <summary>
        /// get list of all products with url
        /// </summary>
        /// <returns></returns>
        ///  <response code="404">If list of products is null</response>
        [HttpGet(Name = nameof(GetAllProductsWithResources))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<ProductWithResource>>> GetAllProductsWithResources()
        {
            var collection = await _productService.GetAllProductsWithResourceAsync();         

            return collection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("/allProducts", Name = nameof(GetProducts))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _productService.GetAllProducts();

            return products;
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
            var productToDelete = await _unitOfWork.Products.GetAsync(x => x.Id == Id);

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
            await _productService.UpdateProductAsync(productId, product);
            
            return Ok();
        }
    }
}
