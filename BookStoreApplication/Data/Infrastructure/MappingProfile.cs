using AutoMapper;
using BookStoreApplicationAPI.Controllers;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Models;
using BookStoreApplicationAPI.Models;

namespace BookStoreApplicationAPI.Data.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<AddUserDto, User>();
            CreateMap<CreateBookingDto, Booking>();
            CreateMap<Product, Product>();
            CreateMap<UserAdressDto, Booking>()
                .ForAllMembers(d => d.Ignore());
            CreateMap<UserAdressDto, CreateBookingDto>()
                .ForMember(d => d.Delivery_Address, opt => opt.MapFrom(s => s.Address));
            CreateMap<BookingRequestDto, Booking>()
            .ForAllMembers(d => d.Ignore());
            CreateMap<BookingRequestDto, Booking>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Product_Id))
                .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Requested_qty));

            CreateMap<Booking, BookingWithProduct>();

            CreateMap<Product, ProductWithResource>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(
                    nameof(ProductsController.GetProduct),
                    new { productId = src.Id }
                    )
                ));
        }
    }
}
