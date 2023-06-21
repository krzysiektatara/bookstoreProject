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
            //this one is helper for other mappers converting from no-nullable to nullable. Otherwise it return 0 for null
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<User, AddUserDto>()
            .ReverseMap()
            .ForAllMembers(opt => opt.Condition((source, dest, sourceMember) => sourceMember != null));

            CreateMap<User, AddProductDto>()
            .ReverseMap()
            .ForAllMembers(opt => opt.Condition((source, dest, sourceMember) => sourceMember != null));

            CreateMap<Product, AddProductDto>()
                .ReverseMap();
            CreateMap<User, AddUserDto>();
            CreateMap<CreateBookingDto, Booking>();
            CreateMap<Product, Product>();
            CreateMap<UserAdressDto, Booking>()
                .ForAllMembers(d => d.Ignore());
            CreateMap<UserAdressDto, CreateBookingDto>()
                .ForMember(d => d.Delivery_Address, opt => opt.MapFrom(s => s.Address));
            //CreateMap<BookingRequestDto, Booking>()
            //.ForAllMembers(d => d.Ignore());
            CreateMap<BookingRequestDto, Booking>()
                .AfterMap((src, dst) =>
                {

                    if (dst.Status_Id == 0)
                    {
                        dst.Status_Id = 1;
                    }
                });

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
