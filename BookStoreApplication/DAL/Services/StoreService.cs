using AutoMapper;
using BookStoreApplicationAPI.Controllers;
using BookStoreApplicationAPI.DAL.UOW;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Exceptions;
using BookStoreApplicationAPI.Data.Models;

namespace BookStoreApplicationAPI.DAL.Services
{
    public class StoreService : IStoreService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        readonly AutoMapper.IConfigurationProvider _mappingConfiguration;
        public StoreService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            AutoMapper.IConfigurationProvider mappingConfiguration
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mappingConfiguration = mappingConfiguration;
        }

        public Task<StoreItem> GetStoreItemByIdAsync(int id)
        {
            return _unitOfWork.Store.GetAsyncById(id);
        }

        public Task<StoreItem> GetStoreItemByProductIdAsync(int productid)
        {
            return _unitOfWork.Store.GetAsync(x => x.ProductId == productid);
        }
        public async Task<StoreItem> AddStoreItem(AddStoreItemDto product)
        {
            var addedStoreItem = _unitOfWork.Store.Add(_mapper.Map<StoreItem>(product));
            _unitOfWork.SaveAsync();
            return addedStoreItem.Result;
        }

        public async Task<StoreItem> AddStoreItemAsync(AddStoreItemDto user)
        {
            var addedStoreItem = await _unitOfWork.Store.AddAsync(_mapper.Map<StoreItem>(user));

            _unitOfWork.SaveAsync();
            return addedStoreItem;
        }

        public async Task UpdateStoreItemAsync(int id, int quantity)
        {
            var item = await _unitOfWork.Store.GetAsyncById(id);
            item.Available_qty += quantity;

            await _unitOfWork.Store.UpdateAsync(item);

            _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
        }

        public async Task RequestStoreItemAsync(int id, int requested_qty)
        {
            var item = await _unitOfWork.Store.GetAsyncById(id);
            if (item.Available_qty < requested_qty)
                throw new RequestedItemIsUnavailableException(requested_qty, item.Available_qty);

            item.Available_qty -= requested_qty;
            await _unitOfWork.Store.UpdateAsync(item);
            _unitOfWork.SaveAsync();
        }

        public async Task DeleteStoreItemAsync(int id)
        {
            var productToDelete = await _unitOfWork.Store.GetAsyncById(id);
            await _unitOfWork.Store.DeleteAsync(productToDelete);
        }


        public async Task<IEnumerable<StoreItem>> GetAllStoreItems()
        {
            var products = await _unitOfWork.Store.GetAllAsync();
            return products;
        }

        public Task<Booking> GetBookingByIdAsync(int id)
        {
            return _unitOfWork.Bookings.GetAsyncById(id);
        }

        public async Task<Booking> CreateBookingAsync(BookingRequestDto bookingForm, int UserId)
        {

            var item = await _unitOfWork.Store.GetAsync(x => x.ProductId == bookingForm.ProductId);
            item.Available_qty -= bookingForm.Quantity;

            var addedBooking = _mapper.Map<Booking>(bookingForm);

            addedBooking.UserId = UserId;

            if (addedBooking.Delivery_Address == null)
            {
                var user = await _unitOfWork.Users.GetAsyncById(UserId);
                addedBooking.Delivery_Address = user.Address;
            }

            await _unitOfWork.Store.UpdateAsync(item);
            _unitOfWork.SaveAsync();
            var a = await _unitOfWork.Bookings.AddAsync(addedBooking);
            _unitOfWork.SaveAsync();


            _unitOfWork.Dispose();
            return addedBooking;
        }
    }
}

