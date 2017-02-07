using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RationBuilder.ViewModels
{
    public class RationViewModel : WindowCreateControlViewModel
    {
        private int authorizationId = -1;
        private IDataManager dataManager = null;
        private RelayCommand editProducts = null;
        private RelayCommand editDishes = null;
        private RelayCommand editSchedules = null;
        private RelayCommand editUserData = null;
        private ObservableCollection<ScheduleViewModel> schedules = null;
        private ScheduleViewModel selectedSchedule = null;
        private Timer timeTimer = new Timer(1000);
        private DateTime timeNow = new DateTime();
        private void LoadSchedulesList()
        {
            schedules.Clear();
            foreach (Schedule f in dataManager.GetScheduleList(authorizationId))
            {
                if (f.DayOfWeek == timeNow.DayOfWeek)
                {
                    schedules.Add(new ScheduleViewModel(f));
                }
            }
        }
        private void SecondElapsed(object obj, ElapsedEventArgs e)
        {
            bool nextDay = false;
            if(timeNow.DayOfWeek != e.SignalTime.DayOfWeek)
            {
                nextDay = true;
            }
            timeNow = e.SignalTime;
            if(nextDay)
            {
                LoadSchedulesList();
            }
            RaisePropertyChanged("Time");
        }
        public IFood DailyRequirement
        {
            get
            {
                Food food = new Food();
                if (dataManager.GetUserDailyActivity(authorizationId) != DailyActivity.None && dataManager.GetUserAge(authorizationId) != 0 &&
                    dataManager.GetUserHeight(authorizationId) != 0 && dataManager.GetUserWeight(authorizationId) != 0)
                {
                    float activity = 1.2F;
                    float purpose = 1;
                    float kiloCalories = 0;
                    int weight = dataManager.GetUserWeight(authorizationId);
                    int height = dataManager.GetUserHeight(authorizationId);
                    int age = dataManager.GetUserAge(authorizationId);
                    switch (dataManager.GetUserDailyActivity(authorizationId))
                    {
                        case DailyActivity.LightActivity:
                            activity = 1.4625F;
                            break;
                        case DailyActivity.MediumActivity:
                            activity = 1.550F;
                            break;
                        case DailyActivity.HighActivity:
                            activity = 1.725F;
                            break;
                        case DailyActivity.ExtremeActivity:
                            activity = 1.9F;
                            break;

                    }
                    switch (dataManager.GetUserPurposeActivity(authorizationId))
                    {
                        case PurposeActivity.None:
                            food.Protein = 1.2F * weight;
                            break;
                        case PurposeActivity.UpWeight:
                            purpose = 1.30F;
                            switch (dataManager.GetUserDailyActivity(authorizationId))
                            {
                                case DailyActivity.NoActivity:
                                    food.Protein = 1.3F * weight;
                                    break;
                                case DailyActivity.LightActivity:
                                    food.Protein = 1.7F * weight;
                                    break;
                                case DailyActivity.MediumActivity:
                                    food.Protein = 1.9F * weight;
                                    break;
                                case DailyActivity.HighActivity:
                                    food.Protein = 2F * weight;
                                    break;
                                case DailyActivity.ExtremeActivity:
                                    food.Protein = 2F * weight;
                                    break;
                            }
                            break;
                        case PurposeActivity.DownWeight:
                            purpose = 0.80F;
                            switch (dataManager.GetUserDailyActivity(authorizationId))
                            {
                                case DailyActivity.NoActivity:
                                    food.Protein = 1.2F * weight;
                                    break;
                                case DailyActivity.LightActivity:
                                    food.Protein = 1.6F * weight;
                                    break;
                                case DailyActivity.MediumActivity:
                                    food.Protein = 1.8F * weight;
                                    break;
                                case DailyActivity.HighActivity:
                                    food.Protein = 1.8F * weight;
                                    break;
                                case DailyActivity.ExtremeActivity:
                                    food.Protein = 2F * weight;
                                    break;
                            }
                            break;
                    }
                    switch (dataManager.GetUserGender(authorizationId))
                    {
                        case Gender.Male:
                            kiloCalories = ((10 * weight + 6.25F * height - 5 * age + 5) * activity) * purpose;
                            break;
                        case Gender.Female:
                            kiloCalories = ((10 * weight + 6.25F * height - 5 * age - 161) * activity) * purpose;
                            break;
                    }
                    food.KiloCalories = kiloCalories;
                    food.Fats = weight * 1.5F;
                    food.Carbohydrates = (kiloCalories - (food.Protein * 4 + food.Fats * 9))/4;
                }
                return food;
            }
        }
        [DependentsUpon("DailyPlan")]
        public IFood DailyPlanLeft
        {
            get
            {
                List<Schedule> schedules = new List<Schedule>();
                foreach(Schedule s in dataManager.GetScheduleList(authorizationId))
                {
                    if (s.DayOfWeek == timeNow.DayOfWeek && timeNow.Hour < s.Hour || (timeNow.Hour == s.Hour && timeNow.Minute < s.Minute))
                    {
                        schedules.Add(s);
                    }
                }
                return CalculatorNutrients.CalculateNutrients(schedules);
            }
        }
        [DependentsUpon("DayOfWeek")]
        public IFood DailyPlan
        {
            get
            {
                List<IFood> food = new List<IFood>();
                foreach (Schedule s in dataManager.GetScheduleList(authorizationId))
                {
                    if (s.DayOfWeek == timeNow.DayOfWeek)
                    {
                        foreach (Food f in s.Food)
                        {
                            food.Add(f);
                        }
                    }
                }
                return CalculatorNutrients.CalculateNutrients(food);
            }
        }
        [DependentsUpon("Time")]
        public string DayOfWeek { get { return DateCharacteristic.GetDayOfWeek(timeNow.DayOfWeek); } }
        public string Time { get { return timeNow.Hour + " : " + timeNow.Minute; } }
        public RelayCommand EditProducts { get { return editProducts; } }
        public RelayCommand EditDishes { get { return editDishes; } }
        public RelayCommand EditSchedules { get { return editSchedules; } }
        public RelayCommand EditUserData { get { return editUserData; } }
        public ObservableCollection<ScheduleViewModel> Schedules { get { return schedules; } set { schedules = value; } }
        public ScheduleViewModel SelectedSchedule { get { return selectedSchedule; } set { SetValue<ScheduleViewModel>(ref selectedSchedule, value); } }
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
        protected override void OnClosed(object obj)
        {
            WindowController.ShutdownApplication();
        }
        public RationViewModel(IWindowFactory windowFactory, IWindowController windowController, IDataManager dataManager, int id)
            : base(windowFactory, windowController)
        {
            timeTimer.Elapsed += SecondElapsed;
            timeTimer.Start();
            timeNow = DateTime.Now;
            schedules = new ObservableCollection<ScheduleViewModel>();
            authorizationId = id;
            this.dataManager = dataManager;
            editProducts = new RelayCommand(_ => { windowFactory.CreateProductsManagerWindow(); LoadSchedulesList(); });
            editDishes = new RelayCommand(_ => {windowFactory.CreateDishesManagerWindow();LoadSchedulesList();});
            editSchedules = new RelayCommand(_ => {windowFactory.CreateScheduleManagerWindow(id);LoadSchedulesList();});
            editUserData = new RelayCommand(_ => { windowFactory.CreateUserEditWindow(id); RaisePropertyChanged("DailyRequirement"); });
            CanExecuteOnClosed = true;
            LoadSchedulesList();
        }
    }
}
