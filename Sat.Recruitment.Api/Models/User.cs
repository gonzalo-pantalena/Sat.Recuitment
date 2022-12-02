using Sat.Recruitment.Api.Models.Enums;
using System;

namespace Sat.Recruitment.Api.Models
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// UserType
        /// </summary>
        public UserType UserType { get; set; }
        /// <summary>
        /// Money
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// User Constructor
        /// </summary>
        public User() { }

        /// <summary>
        /// User Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <param name="userType"></param>
        /// <param name="money"></param>
        public User(string name, string email, string address, string phone, string userType, string money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = Enum.TryParse<UserType>(userType, true, out var type) ? type : UserType.Normal;
            Money = decimal.TryParse(money, out var dMoney) ? dMoney : 0;
        }
    }
}
