using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Domain.Models;

namespace TraderApp.Wpf.State.Accounts
{
    public interface IAccountStore
    {
        event Action StateChanged;
        public Account CurrentAccount { get; set; }
    }
}
