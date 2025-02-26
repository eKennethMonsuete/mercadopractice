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

        public Task UpdateAsync(T entity)
        {
            // Assumindo que T tem uma propriedade Id do tipo long
            var keyProperty = typeof(T).GetProperty("Id");
            if (keyProperty == null)
            {
                throw new InvalidOperationException("Entity does not have an 'Id' property.");
            }

            var itemId = (long)keyProperty.GetValue(entity); // Converta para long, ajustando conforme o tipo da chave
            var result = dbSet.Find(itemId); // Use Find para obter a entidade com base no ID

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(entity);
                    _context.SaveChanges();
                    return entity;
                }
                catch (Exception ex)
                {
                    // Consider logging the exception
                    throw new InvalidOperationException("Update operation failed.", ex);
                }
            }
            else
            {
                throw new KeyNotFoundException("Entity with the given Id was not found.");
            }
        } 
        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
