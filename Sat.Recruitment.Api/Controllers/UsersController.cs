using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Extensions;
using System.Diagnostics;
using System.Linq;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        public const string DuplicatedUserMessage = "The user is duplicated";
        public const string CreatedUserMessage = "User Created";
        public const string NameRequired = "The name is required";
        public const string EmailRequired = "The email is required";
        public const string AddressRequired = "The address is required";
        public const string PhoneRequired = "The phone is required";

        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <param name="userType"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/v1/create-user")]
        [Route("/create-user")]
        public Result CreateUser(string name, string email, string address, string phone, string userType, string money) 
            => CreateUser(new User(name, email, address, phone, userType, money));

        /// <summary>
        /// Create User V2
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/v2/create-user")]
        public Result CreateUser(User newUser)
        {
            if (newUser.HasErrors(out var errors))
            {
                return new Result(false, errors);
            }

            newUser.RecalculateMoney();

            newUser.NormalizeEmail();

            var users = _usersRepository.GetUsers();

            if (!users.Any(u => u.Email == newUser.Email || u.Phone == newUser.Phone || (u.Name == newUser.Name && u.Address == newUser.Address)))
            {
                Debug.WriteLine(CreatedUserMessage);

                return new Result(true, CreatedUserMessage);
            }
            else
            {
                Debug.WriteLine(DuplicatedUserMessage);

                return new Result(false, DuplicatedUserMessage);
            }
        }
    }
}
