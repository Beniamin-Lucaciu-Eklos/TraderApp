using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Application.Services;
using TraderApp.Domain.Models;
using TraderApp.Infrastructure.EF;
using TraderApp.Infrastructure.Services.Common;

namespace TraderApp.Infrastructure.Services
{
    public class DataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly TraderDbDesignTimeOptionsFactory _contextFactory;
        private readonly NonQueryDataService<T> _nonQueryDataService;

        public DataService(TraderDbDesignTimeOptionsFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<T>(contextFactory);
        }

        public async Task<T> CreateAsync(T entity)
        {
            return await _nonQueryDataService.CreateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _nonQueryDataService.DeleteAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T> GetAsync(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
                return entity;
            }
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            return await _nonQueryDataService.UpdateAsync(id, entity);
        }
    }
}
