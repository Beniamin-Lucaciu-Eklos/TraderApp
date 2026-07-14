using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Domain.Exceptions;

namespace TraderApp.Application.Services
{
    public interface IStockPriceService
    {
        /// <summary>
        /// Get the share price for a symbol
        /// </summary>
        /// <param name="symbol">the symbol to get the price of.</param>
        /// <returns>The price of the symbol</returns>
        /// <exception cref="Invalid"></exception>
        Task<decimal> GetPriceAsync(string symbol);
    }
}
