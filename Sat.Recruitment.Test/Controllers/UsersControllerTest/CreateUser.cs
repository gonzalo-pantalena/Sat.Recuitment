using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using Xunit;

namespace Sat.Recruitment.Test.Controllers.UsersControllerTest
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class RecalculateAmount
    {
        private Mock<IUsersRepository> _usersRepository;

        private List<User> _users = new List<User>
        {
            new User("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124")
        };

        private UsersController InitController()
        {
            _usersRepository = new Mock<IUsersRepository>();
            _usersRepository.Setup(r => r.GetUsers()).Returns(_users);

            return new UsersController(_usersRepository.Object);
        }

        [Fact]
        public void Test01()
        {
            var result = InitController().CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354212", "Normal", "124");

            Assert.True(result.IsSuccess);
            Assert.Equal(UsersController.CreatedUserMessage, result.Errors);
        }

        [Fact]
        public void Test02()
        {
            var result = InitController().CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.DuplicatedUserMessage, result.Errors);
        }

        [Fact]
        public void Test03()
        {
            _users[0].Email = "Agustina@gmail.org";

            var result = InitController().CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.DuplicatedUserMessage, result.Errors);
        }

        [Fact]
        public void Test04()
        {
            _users[0].Email = "Agustina@gmail.org";
            _users[0].Email = "+349 1122354214";

            var result = InitController().CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.DuplicatedUserMessage, result.Errors);
        }

        [Fact]
        public void Test05()
        {
            var result = InitController().CreateUser("Agustina", "Agus.tina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.DuplicatedUserMessage, result.Errors);
        }

        [Fact]
        public void Test06()
        {
            var result = InitController().CreateUser("Agustina", "Agustina+Ramos@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.DuplicatedUserMessage, result.Errors);
        }

        [Fact]
        public void Test07()
        {
            var result = InitController().CreateUser(" ", "Agustina+Ramos@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.NameRequired, result.Errors);
        }

        [Fact]
        public void Test08()
        {
            var result = InitController().CreateUser("Agustina", " ", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.EmailRequired, result.Errors);
        }

        [Fact]
        public void Test09()
        {
            var result = InitController().CreateUser("Agustina", " ", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.EmailRequired, result.Errors);
        }

        [Fact]
        public void Test10()
        {
            var result = InitController().CreateUser("Agustina", "Agustina@gmail.com", " ", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.AddressRequired, result.Errors);
        }

        [Fact]
        public void Test11()
        {
            var result = InitController().CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", " ", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.PhoneRequired, result.Errors);
        }

        [Fact]
        public void Test12()
        {
            var result = InitController().CreateUser(null, null, null, null, "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal($"{UsersController.NameRequired}, {UsersController.EmailRequired}, {UsersController.AddressRequired}, {UsersController.PhoneRequired}", result.Errors);
        }
    }
}
