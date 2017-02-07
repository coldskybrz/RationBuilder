using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RationBuilder.Models;

namespace RationBuilder.ViewModels
{
    public class Registration : Base
    {
        //private UserValidation validation = new UserValidation();
        private IUser user = new User();
        private RelayCommand 

        private string login = null;

        [DependentsUpon("Login")]
        public string Error
        {
            get { return login; }//validation.ValidateUserToFormat(user); }
        }
        public string Login { get { return login; } set { login = value; RaisePropertyChanged(); } }

        
    }
}
