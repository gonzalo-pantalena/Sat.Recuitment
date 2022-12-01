using Sat.Recruitment.Api.Controllers.Enums;
using System;

namespace Sat.Recruitment.Api.Controllers.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }

        public User(string name, string email, string address, string phone, string userType, string money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = Enum.Parse<UserType>(userType);
            Money = decimal.Parse(money);
        }
    }
}
