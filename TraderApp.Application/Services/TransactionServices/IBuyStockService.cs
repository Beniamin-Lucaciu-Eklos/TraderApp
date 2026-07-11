using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Domain.Exceptions;
using TraderApp.Domain.Models;

namespace TraderApp.Application.Services.TransactionServices
{
    public interface IBuyStockService
    {
        Task<Account> BuyAsync(Account buyer, string symbol, int shares);
    }    
}
