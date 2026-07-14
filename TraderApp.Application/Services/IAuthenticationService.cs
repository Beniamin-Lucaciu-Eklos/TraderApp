using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Domain.Models;

namespace TraderApp.Application.Services
{
    public enum RegistrationResult
    { 
        Success,
        PasswordsDoNotMatch,
        EmailAlreadyExists,
        UserNameAlreadyExists
    }

    public interface IAuthenticationService
    {
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);

        /// <summary>
        /// Login to application
        /// </summary>
        /// <param name="userName">the user's Name</param>
        /// <param name="password">the user's password</param>
        /// <returns>login by username and password</returns>
        /// <exception cref="UserNotFoundException">thrown if user does not exists</exception>
        /// <exception cref="InvalidPasswordException">thrown if password is invalid</exception>
        /// <exception cref="Exception">login fails</exception>
        Task<Account> Login(string username, string password);
    }
}
