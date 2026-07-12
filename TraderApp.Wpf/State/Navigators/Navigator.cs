using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TraderApp.Wpf.Commands;
using TraderApp.Wpf.ViewModels;
using TraderApp.Wpf.ViewModels.Factories;

namespace TraderApp.Wpf.State.Navigators
{
    public partial class Navigator : ObservableObject, INavigator
    {
        private readonly IRootTraderViewModelFactory _viewModelFactory;

        public Navigator(IRootTraderViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        [ObservableProperty]
        private ViewModelBase _currentViewModel;


        [RelayCommand]
        private void UpdateCurrentViewModel(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
