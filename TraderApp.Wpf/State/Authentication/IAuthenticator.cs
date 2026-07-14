using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Application.Services;
using TraderApp.Domain.Models;
using TraderApp.Domain.Exceptions;

namespace TraderApp.Wpf.State.Authentication
{
    public interface IAuthenticator
    {
        public event Action StateChanged;

        Account CurrentAccount { get; }

        bool IsLoggedIn { get; }

        Task<RegistrationResult> Register(
            string email,
            string userName,
            string password,
            string confirmPassword);

        /// <summary>
        /// Login to application
        /// </summary>
        /// <param name="userName">the user's Name</param>
        /// <param name="password">the user's password</param>
        /// <returns>login by username and password</returns>
        /// <exception cref="UserNotFoundException">thrown if user does not exists</exception>
        /// <exception cref="InvalidPasswordException">thrown if password is invalid</exception>
        /// <exception cref="Exception">login fails</exception>
        Task Login(string userName, string password);

        void LogOut();
    }
}
