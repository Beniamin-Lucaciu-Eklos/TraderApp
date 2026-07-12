using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TraderApp.Application.Services;
using TraderApp.Application.Services.TransactionServices;
using TraderApp.Domain.Models;
using TraderApp.Infrastructure.Services;

namespace TraderApp.Wpf.ViewModels
{
    public partial class BuyViewModel : ViewModelBase
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IBuyStockService _buyStockService;

        public BuyViewModel(
            IStockPriceService stockPriceService,
            IBuyStockService buyStockService)
        {
            _stockPriceService = stockPriceService;
            _buyStockService = buyStockService;
        }

        [ObservableProperty]
        private string _symbol;

        [ObservableProperty]
        private string _searchResultSymbol = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalPrice))]
        private decimal _stockPrice;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalPrice))]
        private int _shareToBuy;

        public decimal TotalPrice => ShareToBuy * StockPrice;

        [RelayCommand]
        public async Task SearchSymbol()
        {
            try
            {
                decimal stockPrice = await _stockPriceService.GetPriceAsync(Symbol);
                StockPrice = stockPrice;
                SearchResultSymbol = Symbol?.ToUpper();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [RelayCommand]
        public async Task BuyStock()
        {
            try
            {
                Account account = await _buyStockService.BuyAsync(new Account()
                {
                    Id = 1,
                    Balance = 500,
                    AssetTransactions = new List<AssetTransaction>
                    { }
                },
                    Symbol,
                    ShareToBuy);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
