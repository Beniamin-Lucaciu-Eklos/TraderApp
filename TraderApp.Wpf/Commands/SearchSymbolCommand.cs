using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TraderApp.Application.Services;
using TraderApp.Wpf.ViewModels;

namespace TraderApp.Wpf.Commands
{
    public class SearchSymbolCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly BuyViewModel _buyViewModel;
        private readonly IStockPriceService _stockPriceService;
        public SearchSymbolCommand(BuyViewModel buyViewModel, IStockPriceService stockPriceService)
        {
            _buyViewModel = buyViewModel;
            _stockPriceService = stockPriceService;
        }

        public bool CanExecute(object? parameter)
            => true;

        public async void Execute(object? parameter)
        {
            try
            {
                decimal stockPrice = await _stockPriceService.GetPriceAsync(_buyViewModel.Symbol);
                _buyViewModel.StockPrice = stockPrice;
                _buyViewModel.SearchResultSymbol = _buyViewModel.Symbol?.ToUpper();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
