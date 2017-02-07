using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RationBuilder.ViewModels
{
    public class LoginViewModel : WindowCreateControlViewModel
    {
        private IAuthorization authorization = null;
        private RelayCommand enter = null;
        private RelayCommand registration = null;
        private RelayCommand passwordChanged = null;
        private string login = null;
        private string password = null;
        private string error = null;
        public RelayCommand Enter { get { return enter; } }
        public RelayCommand Registration { get { return registration; } }
        public RelayCommand PasswordChanged { get { return passwordChanged; } }
        public string Login
        {
            get { return login; }
            set { SetValue<string>(ref login, value); }
        }
        public string Password
        {
            get { return password; }
            set { SetValue<string>(ref password, value); }
        }
        public string Error
        {
            get { return error; }
            set { SetValue<string>(ref error, value); }
        }
        protected override void OnClosed(object obj)
        {
            WindowController.ShutdownApplication();
        }
        public LoginViewModel(IWindowFactory windowFactory, IWindowController windowController, IAuthorization authorization)
            : base(windowFactory, windowController)
        {
            this.authorization = authorization;
            enter = new RelayCommand(EnterExecute);
            registration = new RelayCommand((obj) => WindowFactory.CreateRegisterWindow());
            passwordChanged = new RelayCommand((obj) => Password = (obj as PasswordBox).Password);
        }
        public void EnterExecute(object obj)
        {
            int id = authorization.Login(login,password);
            if (id != -1)
            {
                CanExecuteOnClosed = false;
                WindowController.CloseWindow();
                WindowFactory.CreateRationWindow(id);
            }
            else
            {
                Error = L10n.Resource.LOG_ERROR;
            }
        }
    }
}
