using AutoMapper;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplicationAPI.DAL
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookStoreDbContext context, AutoMapper.IConfigurationProvider mappingConfiguration) : base(context, mappingConfiguration)
        {
        }

    }
}
