using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    public class ScheduleManagerViewModel : WindowCreateControlViewModel
    {
        private IDataManager dataManager = null;
        private RelayCommand add = null;
        private RelayCommand edit = null;
        private RelayCommand delete = null;
        private ObservableCollection<ScheduleViewModel> schedules = null;
        private ScheduleViewModel selectedSchedule = null;
        private int userId = -1;
        private void LoadSchedulesList()
        {
            schedules.Clear();
            foreach(Schedule f in dataManager.GetScheduleList(userId))
            {
                schedules.Add(new ScheduleViewModel(f));
            }
        }
        public ObservableCollection<ScheduleViewModel> Schedules { get { return schedules; } set { schedules = value; } }
        [DependentsUpon("SelectedSchedule")]
        public ObservableCollection<FoodViewModel> Food
        {
            get
            {
                ObservableCollection<FoodViewModel> food = new ObservableCollection<FoodViewModel>();
                if (selectedSchedule != null)
                {
                    foreach (IFood f in selectedSchedule.Food)
                    {
                        food.Add(new FoodViewModel(f));
                    }
                }
                return food;
            }
        }
        public ScheduleViewModel SelectedSchedule { get { return selectedSchedule; } set { SetValue<ScheduleViewModel>(ref selectedSchedule, value); } }
        public RelayCommand Add { get { return add; } }
        [CanExecute("CanEditOrDelete")]
        public RelayCommand Edit { get { return edit; } }
        [CanExecute("CanEditOrDelete")]
        public RelayCommand Delete { get { return delete; } }
        [DependentsUpon("SelectedSchedule")]
        public bool CanEditOrDelete { get { return selectedSchedule != null; } }
        public ScheduleManagerViewModel(IWindowFactory windowFactory, IWindowController windowController, IDataManager dataManager, int userId)
            : base(windowFactory, windowController)
        {
            this.userId = userId;
            this.dataManager = dataManager;
            schedules = new ObservableCollection<ScheduleViewModel>();
            LoadSchedulesList();
            add = new RelayCommand(_ =>
            {
                Schedule model = new Schedule();
                model.UserId = userId;
                windowFactory.CreateScheduleInfoEditorWindow(model, false);
                LoadSchedulesList();
            });
            edit = new RelayCommand(_ =>
            {
                windowFactory.CreateScheduleInfoEditorWindow(dataManager.GetScheduleById(userId,selectedSchedule.Id), true);
                LoadSchedulesList();
            }, _ => CanEditOrDelete);
            delete = new RelayCommand(_ =>
            {
                dataManager.RemoveSchedule(selectedSchedule.UserId,selectedSchedule.Id);
                LoadSchedulesList();
            }, _ => CanEditOrDelete);
        }
    }
}
