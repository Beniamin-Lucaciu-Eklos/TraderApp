using Bogus;
using System;
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
        public async Task<decimal> GetPriceAsync(string symbol)
        {
            await Task.Delay(2400); // Simulate network delay

            var faker = new Faker(CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

            decimal price = faker.Finance.Amount(10M, 90000M);
            return price;
        }
    }
}
