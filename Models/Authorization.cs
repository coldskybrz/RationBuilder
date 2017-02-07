using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public class Authorization : IAuthorization
    {
        private IDataManager dataManager = null;
        public Authorization(IDataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public int Login(string login, string password)
        {
            int id = dataManager.GetIdByLogin(login);
            if(id != -1)
            {
                if(dataManager.GetUserPassword(id) != ComputeHash(password))
                {
                    id = -1;
                }
            }
            return id;
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
