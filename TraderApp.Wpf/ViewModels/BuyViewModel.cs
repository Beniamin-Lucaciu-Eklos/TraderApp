using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TraderApp.Application.Services;
using TraderApp.Application.Services.TransactionServices;
using TraderApp.Domain.Exceptions;
using TraderApp.Domain.Models;
using TraderApp.Infrastructure.Services;
using TraderApp.Wpf.State.Accounts;

namespace TraderApp.Wpf.ViewModels
{
    public partial class BuyViewModel : ViewModelBase
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IBuyStockService _buyStockService;
        private readonly IAccountStore _accountStore;

        public BuyViewModel(
            IStockPriceService stockPriceService,
            IBuyStockService buyStockService,
            IAccountStore accountStore)
        {
            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();

            _stockPriceService = stockPriceService;
            _buyStockService = buyStockService;
            this._accountStore = accountStore;
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

        public MessageViewModel ErrorMessageViewModel { get; }

        public MessageViewModel StatusMessageViewModel { get; }

        [RelayCommand]
        public async Task SearchSymbol()
        {
            ClearMessageViewModels();

            try
            {
                decimal stockPrice = await _stockPriceService.GetPriceAsync(Symbol);
                StockPrice = stockPrice;
                SearchResultSymbol = Symbol?.ToUpper();
            }
            catch (Exception ex)
            {
                ErrorMessageViewModel.Message = ex.Message;
            }
        }

        [RelayCommand]
        public async Task BuyStock()
        {
            ClearMessageViewModels();

            try
            {
                Account account = await _buyStockService.BuyAsync(
                    _accountStore.CurrentAccount,
                    Symbol,
                    ShareToBuy);

                _accountStore.CurrentAccount = account;

                StatusMessageViewModel.Message = $"Successfully purchased {ShareToBuy} shares of {Symbol}";
            }
            catch (InsufficientFundException)
            {
                ErrorMessageViewModel.Message = $"Account has inssuficient funds.Transfer more money to account. Balance {_accountStore.CurrentAccount.Balance}";
            }
            catch (Exception)
            {
                ErrorMessageViewModel.Message = "Transaction failed";
            }
        }

        private void ClearMessageViewModels()
        {
            StatusMessageViewModel.Message = null;
            ErrorMessageViewModel.Message = null;
        }
    }
}
