using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Application.Services;
using TraderApp.Domain.Exceptions;
using TraderApp.Domain.Models;

namespace TraderApp.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthenticationService(IAccountService accountService,
            IPasswordHasher<User> passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        ///  Register user into app
        /// </summary>
        /// <param name="email">the user's email</param>
        /// <param name="username">the user's name</param>
        /// <param name="password">the user's password</param>
        /// <param name="confirmPassword">re enter passaword</param>
        /// <returns>enum entry Registration result</returns>
        /// <exception cref="ArgumentNullException">throw's when any are empty.
        /// </exception>
        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(email);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(username);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(password);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(confirmPassword);

            RegistrationResult result = RegistrationResult.Success;

            if (password != confirmPassword)
                 result = RegistrationResult.PasswordsDoNotMatch;

            Account emailAccount = await _accountService.GetByEmail(email);
            if (emailAccount is not null)
               result = RegistrationResult.EmailAlreadyExists;

            Account usernameAccount = await _accountService.GetByUsername(username);
            if (usernameAccount is not null)
                result = RegistrationResult.UserNameAlreadyExists;

            if (result is RegistrationResult.Success)
            {
                User user = new User
                {
                    Email = email,
                    UserName = username,
                    DateTimeJoined = DateTime.UtcNow,
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, password);

                Account account = new Account()
                {
                    User = user,
                };

                await _accountService.CreateAsync(account);
            }

            return result;
        }

        public async Task<Account> Login(string username, string password)
        {
            Account storedAccount = await _accountService.GetByUsername(username);
            if(storedAccount is null)
                throw new UserNotFoundException(username);

            PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(new User(), storedAccount.User.PasswordHash, password);

            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }

            return storedAccount;
        }
    }
}
