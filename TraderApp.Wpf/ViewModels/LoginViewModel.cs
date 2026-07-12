using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TraderApp.Wpf.Commands;
using TraderApp.Wpf.State.Authentication;

namespace TraderApp.Wpf.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        public LoginViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        private async Task LoginAsync()
        {
            bool success = await _authenticator.Login(UserName, Password);
            if (success)
                MessageBox.Show("succesfully logged");
            else
                MessageBox.Show("login failed");

        }


    }
}
