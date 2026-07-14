using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Application.Services;
using TraderApp.Wpf.State.Authentication;
using TraderApp.Wpf.State.Navigators;

namespace TraderApp.Wpf.ViewModels
{
    public partial class RegisterViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _loginRenavigator;

        public RegisterViewModel(IAuthenticator authenticator,
            IRenavigator loginRenavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();

            _authenticator = authenticator;
            _loginRenavigator = loginRenavigator;
        }

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _confirmPassword;

        public MessageViewModel ErrorMessageViewModel { get; }

        [RelayCommand]
        private async Task Register()
        {
            ErrorMessageViewModel.Message = null;

            var registrationResult = await _authenticator.Register(Email, UserName, Password, ConfirmPassword);
            if (registrationResult != RegistrationResult.Success)
            {
                ErrorMessageViewModel.Message = registrationResult switch
                {
                    RegistrationResult.UserNameAlreadyExists => "User name already exists!",
                    RegistrationResult.EmailAlreadyExists => "Email already exists!",
                    RegistrationResult.PasswordsDoNotMatch => "Passwords doesn't match !",
                    
                    _ => "Registration failed"
                };

                return;
            }

            _loginRenavigator.Renavigate();

        }

        [RelayCommand]
        private async Task ViewLogin()
        {
            _loginRenavigator.Renavigate();
        }
    }
}
