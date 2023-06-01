using AutoMapper;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Exceptions;
using BookStoreApplicationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApplicationAPI.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        internal readonly BookStoreDbContext _context;
        internal IMapper _mapper;
        internal readonly AutoMapper.IConfigurationProvider _mappingConfiguration;
        public GenericRepository(
            BookStoreDbContext context,
            IMapper mapper,
            AutoMapper.IConfigurationProvider mappingConfiguration
            )
        {
            _context = context;
            _mapper = mapper;
            _mappingConfiguration = mappingConfiguration;
        }


        public virtual async Task<ActionResult<T>> GetAsync(int id)
        {
            return (await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id)) ?? throw new ProductNotFoundException(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            var a = await _context.Set<T>().AddAsync(entity);
            return a.Entity;
        }

        public async void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
