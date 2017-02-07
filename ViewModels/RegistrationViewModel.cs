using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RationBuilder.Models;
using System.Windows.Controls;

namespace RationBuilder.ViewModels
{
    public class RegistrationViewModel : WindowCreateControlViewModel
    {
        private UserValidation validation = new UserValidation();
        private HumanCharacteristicViewModel customData = new HumanCharacteristicViewModel();
        private IUser user = new User();
        private IRegistration registration = null;
        private RelayCommand register = null;
        private RelayCommand cancel = null;
        private RelayCommand passwordChanged = null;
        private RelayCommand passwordAgainChanged = null;
        private string passwordAgain = null;

        [CanExecute("CanExecuteRegister")]
        public RelayCommand Register
        {
            get
            {
                return register;
            }
        }
        public RelayCommand Cancel
        {
            get
            {
                return cancel;
            }
        }
        public RelayCommand PasswordChanged { get { return passwordChanged; } }
        public RelayCommand PasswordAgainChanged { get { return passwordAgainChanged; } }
        public HumanCharacteristicViewModel CustomData
        {
            get { return customData; }
        }
        public string Height
        {
            get { return CustomData.SelectedHeight; }
            set
            {
                CustomData.SelectedHeight = value;
                user.Height = CustomData.Height;
                RaisePropertyChanged();
            }
        }
        public string Weight
        {
            get { return CustomData.SelectedWeight; }
            set
            {
                CustomData.SelectedWeight = value;
                user.Weight = CustomData.Weight;
                RaisePropertyChanged();
            }
        }
        public string PurposeActivity
        {
            get { return CustomData.SelectedPurposeActivity; }
            set
            {
                CustomData.SelectedPurposeActivity = value;
                user.PurposeActivity = CustomData.PurposeActivity;
                RaisePropertyChanged();
            }
        }
        public string DailyActivity
        {
            get { return CustomData.SelectedDailyActivity; }
            set
            {
                CustomData.SelectedDailyActivity = value;
                user.DailyActivity = CustomData.DailyActivity;
                RaisePropertyChanged();
            }
        }
        public string Gender
        {
            get { return CustomData.SelectedDailyActivity; }
            set
            {
                CustomData.SelectedGender = value;
                user.Gender = CustomData.Gender;
                RaisePropertyChanged();
            }
        }
        public string Login
        {
            get
            {
                return user.Login;
            }
            set
            {
                user.Login = value;
                RaisePropertyChanged();
            }
        }
        public string Password
        {
            get
            {
                return user.Password;
            }
            set
            {
                user.Password = value;
                RaisePropertyChanged();
            }
        }
        public string PasswordAgain
        {
            get
            {
                return passwordAgain;
            }
            set
            {
                SetValue<string>(ref passwordAgain, value);
            }
        }
        public int Age
        {
            get
            {
                return user.Age;
            }
            set
            {
                user.Age = value;
                RaisePropertyChanged();
            }
        }
        [DependentsUpon("Login")]
        public string Errors
        {
            get
            {
                return validation.HasDataThisLogin(Login);
            }
        }
        [DependentsUpon("Login")]
        [DependentsUpon("Password")]
        [DependentsUpon("PasswordAgain")]
        [DependentsUpon("Age")]
        public bool CanExecuteRegister
        {
            get
            {
                return validation.ComparePassword(user.Password, passwordAgain) == null && validation.ValidateUserToFormat(user) == null;
            }
        }
        [DependentsUpon("CanExecuteRegister")]
        public bool GoodLogin 
        { 
            get { return validation.ValidateLogin(user.Login) == null; } 
        }
        public RegistrationViewModel(IWindowFactory windowFactory, IWindowController windowController, IRegistration registration)
            : base(windowFactory, windowController)
        {
            this.registration = registration;
            register = new RelayCommand(RegisterExecute, _=> CanExecuteRegister);
            cancel = new RelayCommand((obj) => WindowController.CloseWindow());
            passwordChanged = new RelayCommand((obj) => Password = (obj as PasswordBox).Password);
            passwordAgainChanged = new RelayCommand((obj) => PasswordAgain = (obj as PasswordBox).Password);
        }
        public void RegisterExecute(object obj)
        {
            registration.Register(user);
            WindowController.CloseWindow();
        }
        
    }
}
