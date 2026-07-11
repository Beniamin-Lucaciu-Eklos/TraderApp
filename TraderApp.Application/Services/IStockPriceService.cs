using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderApp.Application.Services
{
    public interface IStockPriceService
    {
        Task<decimal> GetPriceAsync(string symbol);
    }
}
