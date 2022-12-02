using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Enums;
using Sat.Recruitment.Api.Repositories;
using System.Collections.Generic;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class RepositoriesUnitTests
    {
        private FileConfig _config = new FileConfig
        {
            FilePath = "Files/Users.txt"
        };
        
        private UsersRepository InitRepository()
        {
            return new UsersRepository(_config);
        }

        [Fact]
        public void Test01()
        {
            var result = InitRepository().GetUsers();

            Assert.Equal("Juan", result[0].Name);
            Assert.Equal("Juan@marmol.com", result[0].Email);
            Assert.Equal("+5491154762312", result[0].Phone);
            Assert.Equal("Peru 2464", result[0].Address);
            Assert.Equal(UserType.Normal, result[0].UserType);
            Assert.Equal(1234, result[0].Money);
        }
    }
}
