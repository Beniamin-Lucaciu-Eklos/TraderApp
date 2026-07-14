using TraderApp.Domain.Models;

namespace TraderApp.Wpf.State.Accounts
{
    public class AccountStore : IAccountStore
    {
        public event Action StateChanged;

        private Account _currentAccount;
        public Account CurrentAccount
        {
            get
            { return _currentAccount; }
            set
            {
                _currentAccount = value;
                StateChanged?.Invoke();
            }
        }
    }
}
