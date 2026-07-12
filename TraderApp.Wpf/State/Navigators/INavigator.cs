using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TraderApp.Wpf.ViewModels;

namespace TraderApp.Wpf.State.Navigators
{
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }

        IRelayCommand<object> UpdateCurrentViewModelCommand { get; }
    }
}
