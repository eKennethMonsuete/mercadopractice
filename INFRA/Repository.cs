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

       

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        } 
        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
