using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Wpf.State.Authentication;
using TraderApp.Wpf.State.Navigators;

namespace TraderApp.Wpf.ViewModels.Factories
{
    public class LoginViewModelFactory : ITraderViewModelFactory<LoginViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LoginViewModelFactory(IAuthenticator authenticator, IRenavigator renavigator)
        {
            _authenticator = authenticator;
            _renavigator = renavigator;
        }

        public LoginViewModel CreateViewModel()
        {
            return new LoginViewModel(_authenticator, _renavigator);
        }
    }
}
