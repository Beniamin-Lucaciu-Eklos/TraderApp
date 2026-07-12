using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TraderApp.Wpf.ViewModels;
using TraderApp.Wpf.ViewModels.Factories;

namespace TraderApp.Wpf.State.Navigators
{
    public partial class Navigator : ObservableObject, INavigator
    {
        [ObservableProperty]
        private ViewModelBase _currentViewModel;
      
    }
}
