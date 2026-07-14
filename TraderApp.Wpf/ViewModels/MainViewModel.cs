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
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;
        public MainViewModel(
            IRootTraderViewModelFactory rootTraderViewModelFactory,
            INavigator navigator,
            IAuthenticator authenticator)
        {
            _rootTraderViewModelFactory = rootTraderViewModelFactory;
            _navigator = navigator;
            _navigator.StateChanged += Navigator_StateChanged;

            _authenticator = authenticator;
            _authenticator.StateChanged += Authenticator_StateChanged;

            StartUI();
        }

        public bool IsLoggedIn => _authenticator.IsLoggedIn;

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _navigator.CurrentViewModel;
            }
            private set
            {
                _navigator.CurrentViewModel = value;
            }
        }

        private void StartUI()
        {
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        [RelayCommand]
        private void UpdateCurrentViewModel(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                CurrentViewModel = _rootTraderViewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
