using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Controllers.Enums;
using Sat.Recruitment.Api.Controllers.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        private readonly List<User> _users = new List<User>();

        public UsersController()
        {
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
        [Route("/create-user")]
        public Result CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var newUser = new User(name, email, address, phone, userType, money);

            if (HasErrors(newUser, out var errors))
            {
                return new Result(false, errors);
            }

            RecalculateAmount(newUser);

            NormalizeEmail(newUser);

            StoreUsersFromFile();

            if (!_users.Any(u => u.Email == newUser.Email || u.Phone == newUser.Phone || (u.Name == newUser.Name && u.Address == newUser.Address)))
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

        //Validate errors
        private bool HasErrors(User user, out string errors)
        {
            var errorlist = new List<string>();

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                errorlist.Add(NameRequired);
            }
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                errorlist.Add(EmailRequired);
            }
            if (string.IsNullOrWhiteSpace(user.Address))
            {
                errorlist.Add(AddressRequired);
            }
            if (string.IsNullOrWhiteSpace(user.Phone))
            {
                errorlist.Add(PhoneRequired);
            }

            errors = string.Join(", ", errorlist);

            return errorlist.Any();
        }

        private static void RecalculateAmount(User newUser)
        {
            switch (newUser.UserType)
            {
                case UserType.Normal:
                    if (newUser.Money > 100)
                    {
                        newUser.Money *= 1.12m;
                    }
                    if (newUser.Money < 100 && newUser.Money > 10)
                    {
                        newUser.Money *= 1.8m;
                    }
                    break;
                case UserType.SuperUser:
                    if (newUser.Money > 100)
                    {
                        newUser.Money *= 1.2m;
                    }
                    break;
                case UserType.Premium:
                    if (newUser.Money > 100)
                    {
                        newUser.Money *= 3;
                    }
                    break;
            }
        }

        private static void NormalizeEmail(User newUser)
        {
            var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            
            aux[0] = aux[0].Replace(".", "");
            
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            if (atIndex >= 0)
            {
                aux[0] = aux[0][..atIndex];
            }

            newUser.Email = $"{aux[0]}@{aux[1]}";
        }

        private void StoreUsersFromFile()
        {
            string[] userData;

            using var reader = ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                userData = reader.ReadLineAsync().Result.Split(',');

                _users.Add(new User(userData[0], userData[1], userData[2], userData[3], userData[4], userData[5]));
            }
        }

        private StreamReader ReadUsersFromFile()
        {
            var fileStream = new FileStream($"{Directory.GetCurrentDirectory()}/Files/Users.txt", FileMode.Open);

            return new StreamReader(fileStream);
        }
    }
}
