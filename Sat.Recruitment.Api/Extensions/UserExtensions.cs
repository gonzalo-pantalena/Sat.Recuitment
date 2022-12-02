using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Api.Extensions
{
    public static class UserExtensions
    {
        public const string NameRequired = "The name is required";
        public const string EmailRequired = "The email is required";
        public const string AddressRequired = "The address is required";
        public const string PhoneRequired = "The phone is required";

        public static bool HasErrors(this User user, out string errors)
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

        public static void RecalculateMoney(this User newUser)
        {
            switch (newUser.UserType)
            {
                case UserType.Normal:
                    if (newUser.Money > 100)
                    {
                        newUser.Money *= 1.12m;
                    }
                    if (newUser.Money <= 100 && newUser.Money > 10)
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

        public static void NormalizeEmail(this User newUser)
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
    }
}
