using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TraderApp.FinancialApi.Services;
using TraderApp.Wpf.State.Navigators;
using TraderApp.Wpf.ViewModels;
using TraderApp.Wpf.ViewModels.Factories;

namespace TraderApp.Wpf.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly IRootTraderViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IRootTraderViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType viewType)
            {
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
