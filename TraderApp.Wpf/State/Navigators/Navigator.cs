using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TraderApp.Wpf.Commands;
using TraderApp.Wpf.Models;
using TraderApp.Wpf.ViewModels;
using TraderApp.Wpf.ViewModels.Factories;

namespace TraderApp.Wpf.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        public Navigator(IRootTraderViewModelFactory viewModelFactory)
        {
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this, viewModelFactory);
        }

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCurrentViewModelCommand { get; set; }
    }
}
