using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TraderApp.Application.Services;
using TraderApp.Application.Services.TransactionServices;
using TraderApp.Infrastructure.Services;
using TraderApp.Wpf.Commands;

namespace TraderApp.Wpf.ViewModels
{
    public class BuyViewModel : ViewModelBase
    {
        public BuyViewModel(
            IStockPriceService stockPriceService,
            IBuyStockService buyStockService)
        {
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            BuyStockCommand = new BuyStockCommand(this, buyStockService);
        }

        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                _symbol = value;
                OnPropertyChanged(nameof(Symbol));
            }
        }

        private string _searchResultSymbol = string.Empty;
        public string SearchResultSymbol
        {
            get { return _searchResultSymbol; }
            set
            {
                _searchResultSymbol = value;
                OnPropertyChanged(nameof(SearchResultSymbol));
            }
        }

        private decimal _stockPrice;
        public decimal StockPrice
        {
            get { return _stockPrice; }
            set
            {
                _stockPrice = value;
                OnPropertyChanged(nameof(StockPrice));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private int _shareToBuy;
        public int ShareToBuy
        {
            get { return _shareToBuy; }
            set
            {
                _shareToBuy = value;
                OnPropertyChanged(nameof(ShareToBuy));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public decimal TotalPrice
        {
            get { return ShareToBuy * StockPrice; }
        }

        public ICommand SearchSymbolCommand { get; }

        public ICommand BuyStockCommand { get; }

    }
}
