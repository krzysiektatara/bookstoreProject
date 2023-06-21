using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Exceptions;
using BookStoreApplicationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace BookStoreApplicationAPI.DAL
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, IEntityBase, new()
    {
        internal readonly BookStoreDbContext _context;
        private readonly DbSet<T> _entitiySet;

        internal readonly AutoMapper.IConfigurationProvider _mappingConfiguration;
        public GenericRepository(
            BookStoreDbContext context,
            AutoMapper.IConfigurationProvider mappingConfiguration
            )
        {
            _context = context;
            _entitiySet = _context.Set<T>();
            _mappingConfiguration = mappingConfiguration;
        }


        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return (await _entitiySet.SingleOrDefaultAsync(expression)) ?? throw new EntityNotFoundException(expression.Name);
        }

        public virtual async Task<T> GetAsyncById(int id)
        {
            return (await _entitiySet.FirstOrDefaultAsync(n => n.Id == id)) ?? throw new EntityNotFoundException(id, typeof(T));
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entitiySet.ToListAsync();
        }


        public virtual async Task<T> Add(T entity)
        {
            return _context.Add(entity).Entity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var a = await _context.AddAsync(entity);
            return a.Entity;
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Remove(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
        }

        public virtual Task Update(T entity, Object? dto = null)
        {
            //todo
            throw new NotImplementedException();
            _context.Update(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
        }


    }
}
