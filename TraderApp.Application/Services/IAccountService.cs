using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Domain.Models;

namespace TraderApp.Application.Services
{
    public interface IAccountService : IDataService<Account>
    {
        Task<Account> GetByUsername(string userName);

        Task<Account> GetByEmail(string email);
    }
}
