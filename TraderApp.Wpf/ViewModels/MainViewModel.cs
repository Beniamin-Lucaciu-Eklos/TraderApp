using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Wpf.State.Authentication;
using TraderApp.Wpf.State.Navigators;
using TraderApp.Wpf.ViewModels.Factories;

namespace TraderApp.Wpf.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {    
        private readonly IRootTraderViewModelFactory _rootTraderViewModelFactory;
        public MainViewModel(
            IRootTraderViewModelFactory rootTraderViewModelFactory,
            INavigator navigator, 
            IAuthenticator authenticator)
        {
            _rootTraderViewModelFactory = rootTraderViewModelFactory;
            Navigator = navigator;
            Authenticator = authenticator;

            StartUI();
        }

        private void StartUI()
        {
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }

        public INavigator Navigator { get; private set; }

        public IAuthenticator Authenticator { get; }

        [RelayCommand]
        private void UpdateCurrentViewModel(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                Navigator.CurrentViewModel = _rootTraderViewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
