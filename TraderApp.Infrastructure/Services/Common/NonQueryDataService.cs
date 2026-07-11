using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Domain.Models;
using TraderApp.Infrastructure.EF;

namespace TraderApp.Infrastructure.Services.Common
{
    public class NonQueryDataService<T> where T : DomainObject
    {
        private readonly TraderDbDesignTimeOptionsFactory _contextFactory;

        public NonQueryDataService(TraderDbDesignTimeOptionsFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> CreateAsync(T entity)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> createdEntity = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdEntity.Entity;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
