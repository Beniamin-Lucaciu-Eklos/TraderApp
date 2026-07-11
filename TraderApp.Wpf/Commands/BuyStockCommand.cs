using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TraderApp.Application.Services.TransactionServices;
using TraderApp.Domain.Models;
using TraderApp.Wpf.ViewModels;

namespace TraderApp.Wpf.Commands
{
    public class BuyStockCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly BuyViewModel _viewModel;
        private readonly IBuyStockService _buyStockService;

        public BuyStockCommand(BuyViewModel viewModel, IBuyStockService buyStockService)
        {
            _viewModel = viewModel;
            _buyStockService = buyStockService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
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
                    _viewModel.Symbol,
                    _viewModel.ShareToBuy);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
