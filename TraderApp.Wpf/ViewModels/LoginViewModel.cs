using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TraderApp.Wpf.State.Authentication;
using TraderApp.Wpf.State.Navigators;

namespace TraderApp.Wpf.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LoginViewModel(IAuthenticator authenticator, 
            IRenavigator renavigator)
        {
            _authenticator = authenticator;
            this._renavigator = renavigator;
        }

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        private async Task LoginAsync()
        {
            bool success = await _authenticator.Login(UserName, Password);

            if (!success)
            {
                MessageBox.Show($"Failed login for userName {UserName}", "Failed");
                return;
            }

            _renavigator.Renavigate();
          //  _navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Home);
        }
    }
}
