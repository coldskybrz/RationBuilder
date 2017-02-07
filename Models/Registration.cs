using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public class Registration : IRegistration
    {
        private IDataManager dataManager = null;
        public Registration(IDataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public void Register(IUser user)
        {
            List<User> users = dataManager.GetUserList().ToList();
            user.Password = ComputeHash(user.Password);
            int usersCount = users.Count;
            if (usersCount > 0)
            {
                user.Id = users[usersCount - 1].Id + 1;
            }
            dataManager.AddUser((user as User));
        }
        private string ComputeHash(string input)
        {
            byte[] hash;
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);

            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(inputBytes);
            }

            var stringBuilder = new StringBuilder();
            for (var index = 0; index < hash.Length; ++index)
            {
                stringBuilder.Append(hash[index].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
