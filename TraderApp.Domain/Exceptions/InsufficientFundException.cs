using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderApp.Domain.Exceptions
{
    public class InsufficientFundException : Exception
    {
        public decimal AccountBalance { get; }

        public decimal RequiredBalance { get; }

        public InsufficientFundException(decimal accountBalance, decimal requiredBalance)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }
        public InsufficientFundException(decimal accountBalance, decimal requiredBalance, string message)
            : base(message)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }

        public InsufficientFundException(decimal accountBalance, decimal requiredBalance, string message, Exception innerException)
            : base(message, innerException)
        {
            AccountBalance = accountBalance;
            RequiredBalance = requiredBalance;
        }
    }
}
