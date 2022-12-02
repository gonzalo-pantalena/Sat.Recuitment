using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Api.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string _filePath;

        public UsersRepository(FileConfig fileConfig) 
        {
            _filePath = fileConfig.FilePath;
        }

        public List<User> GetUsers()
        {
            string[] userData;
            var users = new List<User>();

            using var reader = ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                userData = reader.ReadLineAsync().Result.Split(',');

                users.Add(new User(userData[0], userData[1], userData[2], userData[3], userData[4], userData[5]));
            }

            return users;
        }

        private StreamReader ReadUsersFromFile()
        {
            var fileStream = new FileStream(_filePath, FileMode.Open);

            return new StreamReader(fileStream);
        }
    }
}
