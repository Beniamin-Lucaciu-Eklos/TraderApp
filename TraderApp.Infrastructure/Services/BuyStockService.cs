using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Application.Services;
using TraderApp.Application.Services.TransactionServices;
using TraderApp.Domain.Exceptions;
using TraderApp.Domain.Models;

namespace TraderApp.Infrastructure.Services
{
    public class BuyStockService : IBuyStockService
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IDataService<Account> _accountService;

        public BuyStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            _stockPriceService = stockPriceService;
            _accountService = accountService;
        }

        public async Task<Account> BuyAsync(Account buyer, string symbol, int shares)
        {
            decimal stockPrice = await _stockPriceService.GetPriceAsync(symbol);

            decimal transactionPrice = stockPrice * shares;
            if (transactionPrice > buyer.Balance)
                throw new InsufficientFundException(buyer.Balance, transactionPrice);


            AssetTransaction transaction = new AssetTransaction
            {
                Account = buyer,
                Asset = new Asset
                {
                    Symbol = symbol,
                    PricePerShare = stockPrice
                },
                DateProcessed = DateTime.UtcNow,
                Shares = shares,
                IsPurchase = true
            };

            buyer.AssetTransactions.Add(transaction);
            buyer.Balance -= transactionPrice;

            await _accountService.UpdateAsync(buyer.Id, buyer);

            return buyer;
        }
    }
}
