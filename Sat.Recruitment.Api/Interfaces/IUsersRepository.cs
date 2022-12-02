using Sat.Recruitment.Api.Models;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Interfaces
{
    public interface IUsersRepository
    {
        List<User> GetUsers();
    }
}
