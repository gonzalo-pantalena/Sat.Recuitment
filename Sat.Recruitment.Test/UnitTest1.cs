using Sat.Recruitment.Api.Controllers;

using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private readonly UsersController userController = new UsersController();

        [Fact]
        public void Test01()
        {
            var result = userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.True(result.IsSuccess);
            Assert.Equal(UsersController.CreatedUserMessage, result.Errors);
        }

        [Fact]
        public void Test02()
        {
            var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.DuplicatedUserMessage, result.Errors);
        }

        [Fact]
        public void Test03()
        {
            var result = userController.CreateUser("Agustina", "Agus.tina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.DuplicatedUserMessage, result.Errors);
        }

        [Fact]
        public void Test04()
        {
            var result = userController.CreateUser("Agustina", "Agustina+Ramos@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.DuplicatedUserMessage, result.Errors);
        }

        [Fact]
        public void Test05()
        {
            var result = userController.CreateUser(" ", "Agustina+Ramos@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.NameRequired, result.Errors);
        }

        [Fact]
        public void Test06()
        {
            var result = userController.CreateUser("Agustina", " ", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.EmailRequired, result.Errors);
        }

        [Fact]
        public void Test07()
        {
            var result = userController.CreateUser("Agustina", " ", "Av. Juan G", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.EmailRequired, result.Errors);
        }

        [Fact]
        public void Test08()
        {
            var result = userController.CreateUser("Agustina", "Agustina@gmail.com", " ", "+349 1122354215", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.AddressRequired, result.Errors);
        }

        [Fact]
        public void Test09()
        {
            var result = userController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", " ", "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal(UsersController.PhoneRequired, result.Errors);
        }

        [Fact]
        public void Test10()
        {
            var result = userController.CreateUser(null, null, null, null, "Normal", "124");

            Assert.False(result.IsSuccess);
            Assert.Equal($"{UsersController.NameRequired}, {UsersController.EmailRequired}, {UsersController.AddressRequired}, {UsersController.PhoneRequired}", result.Errors);
        }
    }
}
