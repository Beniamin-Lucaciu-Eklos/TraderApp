using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TraderApp.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public InvalidPasswordException(string? message, string username, string password)
            : base(message)
        {
            Username = username;
            Password = password;
        }

        public InvalidPasswordException(string? message, Exception? innerException,
            string username, string password)
            : base(message, innerException)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }
}
