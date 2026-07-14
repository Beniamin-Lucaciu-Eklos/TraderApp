using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TraderApp.Domain.Exceptions;
using TraderApp.Wpf.State.Authentication;
using TraderApp.Wpf.State.Navigators;

namespace TraderApp.Wpf.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _loginRenavigator;
        private readonly IRenavigator _registerRenavigator;

        public LoginViewModel(IAuthenticator authenticator,
            IRenavigator loginRenavigator,
            IRenavigator registerRenavigator)
        {
            _authenticator = authenticator;
            this._loginRenavigator = loginRenavigator;

            ErrorMessageViewModel = new MessageViewModel();
            _registerRenavigator = registerRenavigator;
        }

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        public MessageViewModel ErrorMessageViewModel { get; }

        [RelayCommand]
        private async Task LoginAsync()
        {
            ErrorMessageViewModel.Message = null;

            try
            {
                await _authenticator.Login(UserName, Password);
                _loginRenavigator.Renavigate();
            }
            catch (UserNotFoundException)
            {
                ErrorMessageViewModel.Message = $"{UserName} not found";
            }
            catch (InvalidPasswordException)
            {
                ErrorMessageViewModel.Message = $"Incorrect password";
            }
            catch (Exception)
            {
                ErrorMessageViewModel.Message = $"Failed login for userName {UserName} ";
            }
        }

        [RelayCommand]
        private void ViewRegister()
        {
            _registerRenavigator.Renavigate();
        }
    }
}
