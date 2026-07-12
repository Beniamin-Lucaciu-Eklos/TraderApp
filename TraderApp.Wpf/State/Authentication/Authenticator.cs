using CommunityToolkit.Mvvm.ComponentModel;
using TraderApp.Application.Services;
using TraderApp.Domain.Models;
namespace TraderApp.Wpf.State.Authentication
{
    public partial class Authenticator : ObservableObject,
        IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;

        public Authenticator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsLoggedIn))]
        private Account _currentAccount;

        public bool IsLoggedIn => CurrentAccount is not null;

        public async Task<bool> Login(string userName, string password)
        {
            bool success = true;

            try
            {
                CurrentAccount = await _authenticationService.Login(userName, password);
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public void LogOut()
        {
            CurrentAccount = null;
        }

        public async Task<RegistrationResult> Register(
            string email,
            string userName,
            string password,
            string confirmPassword)
        {
            return await _authenticationService.Register(email, userName, password, confirmPassword);
        }
    }
}
