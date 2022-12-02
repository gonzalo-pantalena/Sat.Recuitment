using Sat.Recruitment.Api.Extensions;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Enums;
using Xunit;

namespace Sat.Recruitment.Test.Extensions.UserExtensions
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class RecalculateMoney
    {
        private User _user = new User("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", null, null);


        [Fact]
        public void Test01()
        {
            _user.RecalculateMoney();

            
            Assert.Equal(0m, _user.Money);
        }

        [Fact]
        public void Test02()
        {
            _user.Money = 9m;

            _user.RecalculateMoney();

            Assert.Equal(9m, _user.Money);
        }

        [Fact]
        public void Test03()
        {
            _user.Money = 11m;

            _user.RecalculateMoney();

            Assert.Equal(19.8m, _user.Money);
        }

        [Fact]
        public void Test04()
        {
            _user.Money = 101m;

            _user.RecalculateMoney();

            Assert.Equal(113.12m, _user.Money);
        }

        [Fact]
        public void Test05()
        {
            _user.UserType = UserType.SuperUser;

            _user.RecalculateMoney();

            Assert.Equal(0m, _user.Money);
        }

        [Fact]
        public void Test06()
        {
            _user.UserType = UserType.SuperUser;
            _user.Money = 10m;

            _user.RecalculateMoney();

            Assert.Equal(10m, _user.Money);
        }

        [Fact]
        public void Test07()
        {
            _user.UserType = UserType.SuperUser;
            _user.Money = 101m;

            _user.RecalculateMoney();

            Assert.Equal(121.2m, _user.Money);
        }

        [Fact]
        public void Test08()
        {
            _user.UserType = UserType.Premium;

            _user.RecalculateMoney();

            Assert.Equal(0m, _user.Money);
        }

        [Fact]
        public void Test09()
        {
            _user.UserType = UserType.Premium;
            _user.Money = 10m;

            _user.RecalculateMoney();

            Assert.Equal(10m, _user.Money);
        }

        [Fact]
        public void Test10()
        {
            _user.UserType = UserType.Premium;
            _user.Money = 101m;

            _user.RecalculateMoney();

            Assert.Equal(303m, _user.Money);
        }
    }
}
