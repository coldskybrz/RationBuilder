using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public class UserValidation
    {
        private IDataManager dataManager = new DataManager();
        public List<string> ValidateUser(IUser user)
        {
            List<string> errors = new List<string>();
            Action<string> AddErrorIfHas = x => { if (x != null) { errors.Add(x); } };
            AddErrorIfHas(ValidateLogin(user.Login));
            AddErrorIfHas(HasDataThisLogin(user.Login));
            AddErrorIfHas(ValidatePassword(user.Password));
            AddErrorIfHas(ValidateAge(user.Age));
            return errors;
        }
        public string ValidateUserToFormat(IUser user)
        {
            string error = null;
            List<string> errors = ValidateUser(user);
            if(errors.Count != 0)
            {
                error = "";
                foreach(string e in errors)
                {
                    error += e + Environment.NewLine;
                }
            }
            return error;
        }
        public string ValidateLogin(string login)
        {
            string error = null;
            if (login == null || !Regex.IsMatch(login, @"[a-zA-Z0-9_]{4,}$"))
            {
                error = L10n.Resource.REG_LOG_ERROR;
            }
            return error;
        }
        public string HasDataThisLogin(string login)
        {
            string error = null;
            if(dataManager.GetIdByLogin(login) != -1)
            {
                error = L10n.Resource.REG_LOG_BUSY_ERROR;
            }
            return error;
        }
        public string ValidatePassword(string password)
        {
            string error = null;
            if (password == null || !Regex.IsMatch(password, @"[a-zA-Z0-9_]{4,}$"))
            {
                error = L10n.Resource.REG_PAS_ERROR;
            }
            return error;
        }
        public string ComparePassword(string pasFirst, string pasSecond)
        {
            string error = null;
            if (pasFirst != pasSecond)
            {
                error = L10n.Resource.REG_PAS_A_ERROR;
            }
            return error;
        }
        public string ValidateAge(int age)
        {
            string error = null;
            if (age < 0 || !Regex.IsMatch(age.ToString(), @"[0-9]{1,}$"))
            {
                error = L10n.Resource.REG_AGE_ERROR;
            }
            return error;
        }
    }
}
