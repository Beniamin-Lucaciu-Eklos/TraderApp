using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TraderApp.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string UserName { get; }

        public UserNotFoundException(string userName)
        {
            UserName = userName;
        }

        public UserNotFoundException(string? message, string userName) : base(message)
        {
            UserName = userName;
        }

        public UserNotFoundException(string? message, Exception? innerException, string userName)
            : base(message, innerException)
        {
            UserName = userName;
        }
    }
}
