using Bogus;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Application.Services;

namespace TraderApp.FinancialApi.Services
{
    public class MockStockPriceService : IStockPriceService
    {
        private static readonly Random _random = new Random();
        private readonly ConcurrentDictionary<string, decimal> _basePrices = new ConcurrentDictionary<string, decimal>();

        public async Task<decimal> GetPriceAsync(string symbol)
        {
            await Task.Delay(2400); 
               
            decimal basePrice = _basePrices.GetOrAdd(symbol, s => GenerateSeedPrice(s));

            double changePercent = (_random.NextDouble() * 4.0 - 2.0) / 100.0; // -2% to +2%
            decimal newPrice = basePrice * (1 + (decimal)changePercent);

   
            newPrice = Math.Max(newPrice, 0.01M);

            _basePrices[symbol] = newPrice;

            return Math.Round(newPrice, 2);
        }

        private decimal GenerateSeedPrice(string symbol)
        {
            int hash = symbol.GetHashCode();
            var seededRandom = new Random(hash);
            return (decimal)(seededRandom.NextDouble() * (900 - 10) + 10);
        }
    }
}
