using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderApp.Application.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(int id, T entity);

        Task<bool> DeleteAsync(int id);
    }
}
