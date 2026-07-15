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

        /// <summary>
        ///  Register user into app
        /// </summary>
        /// <param name="email">the user's email</param>
        /// <param name="username">the user's name</param>
        /// <param name="password">the user's password</param>
        /// <param name="confirmPassword">re enter passaword</param>
        /// <returns>enum entry Registration result</returns>
        /// <exception cref="ArgumentNullException">throw's when any <paramref name="email"/>
        /// <paramref name="username"/>
        /// <paramref name="password"/>
        /// <paramref name="confirmPassword"/>
        ///  are empty.
        /// </exception>
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
