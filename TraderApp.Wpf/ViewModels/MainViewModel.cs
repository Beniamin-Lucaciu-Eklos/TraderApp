using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Wpf.State.Authentication;
using TraderApp.Wpf.State.Navigators;

namespace TraderApp.Wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {     
        public MainViewModel(INavigator navigator, IAuthenticator authenticator)
        {
            Navigator = navigator;
            Authenticator = authenticator;

            StartUI();
        }

        private void StartUI()
        {
            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }

        public INavigator Navigator { get; private set; }

        public IAuthenticator Authenticator { get; }
    }
}
