using DOMAIN;
using INFRA.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFRA
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private MyDbContext _context;

        private DbSet<T> dbSet;

        public Repository(MyDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
              dbSet.Add(entity);
              await _context.SaveChangesAsync();
              return entity;
                                           
        }



        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public Task<T?> GetByIdAsync(long id)
        {
            return _context.Set<T>().FindAsync(id).AsTask();
        }

        public async Task UpdateAsync( long id, T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(id);

            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Entidade com ID {id} não encontrada.");
            }

            _context.Update(entity);
            await _context.SaveChangesAsync();

        } 
        public void DeleteAsync(long id)
        {
            var entity = dbSet.SingleOrDefault(e => EF.Property<long>(e, "Id") == id);
            dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
