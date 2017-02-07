using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    public class UserEditViewModel : WindowControllableViewModel
    {
        private IDataManager dataManager = null;
        private HumanCharacteristicViewModel customData = null;
        private RelayCommand ready = null;
        private RelayCommand cancel = null;
        private int age = 0;
        private int id = -1;
        public HumanCharacteristicViewModel CustomData { get { return customData; } }
        [CanExecute("CanReadyExecute")]
        public RelayCommand Ready { get { return ready; } }
        public RelayCommand Cancel { get { return cancel; } }
        public int Age { get { return age; } set { SetValue<int>(ref age, value); } }
        [DependentsUpon("Age")]
        public bool CanReadyExecute { get { return age >= 0; } }
        public UserEditViewModel(IWindowController windowController, IDataManager dataManager, int id)
            : base(windowController)
        {
            this.dataManager = dataManager;
            this.id = id;
            age = dataManager.GetUserAge(id);
            ready = new RelayCommand(ReadyExecute, _=> CanReadyExecute);
            cancel = new RelayCommand(_ => windowController.CloseWindow());
            customData = new HumanCharacteristicViewModel(dataManager.GetUser(id));
        }
        private void ReadyExecute(object obj)
        {
            dataManager.SetUserDailyActivity(id, customData.DailyActivity);
            dataManager.SetUserPurposeActivity(id, customData.PurposeActivity);
            dataManager.SetUserGender(id, customData.Gender);
            dataManager.SetUserHeight(id, customData.Height);
            dataManager.SetUserWeight(id, customData.Weight);
            dataManager.SetUserAge(id, Age);
            WindowController.CloseWindow();
        }
    }
}
