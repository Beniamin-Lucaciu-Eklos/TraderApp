using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Application.Services;
using TraderApp.Domain.Models;

namespace TraderApp.Wpf.State.Authentication
{
    public interface IAuthenticator
    {
        Account CurrentAccount { get; }

        bool IsLoggedIn { get; }

        Task<RegistrationResult> Register(
            string email,
            string userName,
            string password,
            string confirmPassword);

        Task<bool> Login(string userName, string password);

        void LogOut();
    }
}
