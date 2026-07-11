using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Application.Services;
using TraderApp.Domain.Exceptions;
using TraderApp.Domain.Models;
using TraderApp.Infrastructure.Services;

namespace TradeApp.Infrastructure.Tests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly Mock<IPasswordHasher<User>> _mockPassword;
        private readonly AuthenticationService _authenticationService;

        public AuthenticationServiceTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockPassword = new Mock<IPasswordHasher<User>>();
            _authenticationService = new AuthenticationService(_mockAccountService.Object, _mockPassword.Object);
        }

        [Fact]
        public async Task Login_WithCorrectPasswordForExistingUser_ReturnAccountForUserName()
        {
            //AAA
            string expactedUsername = "testUser";
            string password = "testPassword";

            _mockAccountService.Setup(s => s.GetByUsername(expactedUsername))
                              .ReturnsAsync(() => new Account { User = new User { UserName = expactedUsername } });

            _mockPassword.Setup(x => x.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), password))
                        .Returns(() => PasswordVerificationResult.Success);

            var account = await _authenticationService.Login(expactedUsername, password);

            account.Should().NotBeNull();
            account.User.Should().NotBeNull();
            account.User.UserName.Should().Be(expactedUsername);
            // account.User.PasswordHash.Should().Be(password);
        }

        [Fact]
        public async Task Login_WithInCorrectPasswordForExistingUser_ThrowInvalidPasswordException()
        {
            //AAA
            string expactedUsername = "testUser";
            string password = "testPassword";

            _mockAccountService.Setup(s => s.GetByUsername(expactedUsername))
                              .ReturnsAsync(() => new Account { User = new User { UserName = expactedUsername } });

            _mockPassword.Setup(x => x.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), password))
                        .Returns(() => PasswordVerificationResult.Failed);


            Func<Task> action = async () => await _authenticationService.Login(expactedUsername, password);

            await action.Should().ThrowAsync<InvalidPasswordException>();
        }

        [Fact]
        public async Task Login_NoneExistingUser_ThrowsUserNotFoundException()
        {
            //AAA
            string expactedUsername = "testUser";
            string password = "testPassword";

            _mockPassword.Setup(x => x.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), password))
                        .Returns(() => PasswordVerificationResult.Failed);


            Func<Task> action = async () => await _authenticationService.Login(expactedUsername, password);
            await action.Should().ThrowAsync<UserNotFoundException>();
        }

        [Fact]
        public async Task Register_WithPasswordNotMatching_ReturnsPasswordsDoNotMatch()
        {
            RegistrationResult expacted = RegistrationResult.PasswordsDoNotMatch;
            string password = "password1";
            string confirmPassword = "password2";

            var registrationResult = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword);
            registrationResult.Should().Be(expacted);
        }

        [Fact]
        public async Task Register_WithExistingEmail_ReturnsEmailAlreadyExists()
        {
            string email = "a@a.ro";
            _mockAccountService.Setup(a => a.GetByEmail(email))
                .ReturnsAsync(new Account());

            RegistrationResult expacted = RegistrationResult.EmailAlreadyExists;
            var registrationResult = await _authenticationService.Register(email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            registrationResult.Should().Be(expacted);
        }

        [Fact]
        public async Task Register_WithAlreadyExistingEmail_ReturnsUserNameAlreadyExists()
        {
            string userName = "testUser";
            _mockAccountService.Setup(s => s.GetByUsername(userName))
                .ReturnsAsync(new Account());

            RegistrationResult expacted = RegistrationResult.UserNameAlreadyExists;
            var registrationResult = await _authenticationService.Register(It.IsAny<string>(), userName, "test", "test");
            registrationResult.Should().Be(expacted);
        }

        [Fact]
        public async Task Register_WithNoneExistingUserWithMatchPasswords_ReturnsSuccess()
        {
            RegistrationResult expacted = RegistrationResult.Success;
            var registrationResult = await _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), "test", "test");
            registrationResult.Should().Be(expacted);
        }
    }
}
