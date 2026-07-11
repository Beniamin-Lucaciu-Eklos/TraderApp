using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Wpf.State.Navigators;

namespace TraderApp.Wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {     
        public MainViewModel(INavigator navigator)
        {
            Navigator = navigator;
        }

        public INavigator Navigator { get; private set; }
    }
}
