using AutoMapper;
using BookStoreApplicationAPI.Controllers;
using BookStoreApplicationAPI.DAL.UOW;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Models;

namespace BookStoreApplicationAPI.DAL.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        readonly AutoMapper.IConfigurationProvider _mappingConfiguration;
        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            AutoMapper.IConfigurationProvider mappingConfiguration
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mappingConfiguration = mappingConfiguration;
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            return _unitOfWork.Products.GetAsyncById(id);
        }
        public async Task<Product> AddProduct(AddProductDto product)
        {
            var addedProduct = _unitOfWork.Products.Add(_mapper.Map<Product>(product));
            _unitOfWork.SaveAsync();
            return addedProduct.Result;
        }

        public async Task<Product> AddProductAsync(AddProductDto user)
        {
            var addedProduct = await _unitOfWork.Products.AddAsync(_mapper.Map<Product>(user));

            _unitOfWork.SaveAsync();
            return addedProduct;
        }

        public async Task UpdateProductAsync(int id, AddProductDto? dto)
        {
            var product = await _unitOfWork.Products.GetAsyncById(id);

            await _unitOfWork.Products.UpdateAsync(_mapper.Map(dto, product));

            _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
        }

        public async Task DeleteProductAsync(int id)
        {
            var productToDelete = await _unitOfWork.Products.GetAsyncById(id);
            await _unitOfWork.Products.DeleteAsync(productToDelete);
        }

        public async Task<Collection<ProductWithResource>> GetAllProductsWithResourceAsync()
        {
            var products = await _unitOfWork.Products.GetAllWithResourceAsync();

            var collection = new Collection<ProductWithResource>()
            {
                Self = Link.ToCollection(nameof(ProductsController.GetAllProductsWithResources)),
                Value = products.ToArray(),

            };
            return collection;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return products;
        }
    }

}

