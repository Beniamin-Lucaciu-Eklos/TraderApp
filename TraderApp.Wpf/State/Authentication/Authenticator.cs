using CommunityToolkit.Mvvm.ComponentModel;
using TraderApp.Application.Services;
using TraderApp.Domain.Models;
using TraderApp.Wpf.State.Accounts;
namespace TraderApp.Wpf.State.Authentication
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountStore _accountStore;
        public event Action StateChanged;

        public Authenticator(IAuthenticationService authenticationService,
            IAccountStore accountStore)
        {
            _authenticationService = authenticationService;
            _accountStore = accountStore;
        }

        public Account CurrentAccount
        {
            get { return _accountStore.CurrentAccount; }
            private set
            {
                _accountStore.CurrentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentAccount is not null;

        public async Task Login(string userName, string password)
        {
            CurrentAccount = await _authenticationService.Login(userName, password);
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
