using AutoMapper;
using BookStoreApplication.Models;
using BookStoreApplicationAPI.Controllers;
using BookStoreApplicationAPI.Models;

namespace BookStoreApplicationAPI.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<AddUserDto, UserEntity>();
            CreateMap<CreateBookingDto, BookingEntity>();
            CreateMap<AddProductDto, ProductEntity>();
            CreateMap<UserAdressDto, BookingEntity>()
                .ForAllMembers(d => d.Ignore());
            CreateMap<UserAdressDto, CreateBookingDto>()
                .ForMember(d => d.Delivery_Address, opt => opt.MapFrom(s => s.Address));
            CreateMap<BookingRequestDto, BookingEntity>()
            .ForAllMembers(d => d.Ignore());
            CreateMap<BookingRequestDto, BookingEntity>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Product_Id))
                .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Requested_qty));

            CreateMap<BookingEntity, Booking>();

            CreateMap<ProductEntity, Product>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(
                    nameof(ProductsController.GetProduct),
                    new { productId = src.Id }
                    )
                ));
        }
    }
}
