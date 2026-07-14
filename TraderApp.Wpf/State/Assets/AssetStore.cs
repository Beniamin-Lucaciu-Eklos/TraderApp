using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Domain.Models;
using TraderApp.Wpf.State.Accounts;

namespace TraderApp.Wpf.State.Assets
{
    public class AssetStore
    {
        private readonly IAccountStore _accountStore;
        public event Action StateChanged;

        public AssetStore(IAccountStore accountStore)
        {
            _accountStore = accountStore;

            _accountStore.StateChanged += AccountStore_StateChanged;
        }

        public decimal AccountBalance => _accountStore.CurrentAccount?.Balance
            ?? 0;

        public IEnumerable<AssetTransaction> AssetTransactions =>
            _accountStore.CurrentAccount?.AssetTransactions
            ?? new List<AssetTransaction>();

        private void AccountStore_StateChanged()
        {
            StateChanged?.Invoke();        
        }
    }
}
