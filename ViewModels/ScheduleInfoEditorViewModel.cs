using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    public class ScheduleInfoEditorViewModel : WindowCreateControlViewModel
    {
        private IDataManager dataManager = null;
        private DateCharacteristic date = new DateCharacteristic();
        private RelayCommand ready = null;
        private RelayCommand cancel = null;
        private RelayCommand addProduct = null;
        private RelayCommand addDish = null;
        private RelayCommand delete = null;
        private ScheduleViewModel schedule = null;
        private DishViewModel selectedDish = null;
        private FoodViewModel selectedProduct = null;
        private FoodViewModel selectedFood = null;
        private ObservableCollection<FoodViewModel> products = null;
        private ObservableCollection<DishViewModel> dishes = null;
        private ObservableCollection<FoodViewModel> food = null;
        private string selectedDayOfWeek = null;
        private string selectedMinute = null;
        private string selectedHour = null;
        private bool edit = false;
        private Schedule scheduleModel = null;
        public DateCharacteristic Date{get{return date;}set{date = value;}}
        public ObservableCollection<string> DaysOfWeek{get{return date.DaysOfWeekitems;}}
        public ObservableCollection<string> Hourse{get{return date.HourseItems;}}
        public ObservableCollection<string> Minutes{get{return date.MinuteItems;}}
        public string SelectedDayOfWeek{get{return selectedDayOfWeek;} set{selectedDayOfWeek = value;RaisePropertyChanged();}}
        public string SelectedMinute {get{return selectedMinute;} set{selectedMinute = value;RaisePropertyChanged();}}
        public string SelectedHour {get{return selectedHour;} set{selectedHour = value;RaisePropertyChanged();}}
        public ScheduleViewModel Schedule 
        {
            get { return schedule; } 
            set 
            {
                SetValue<ScheduleViewModel>(ref schedule, value);
            }
        }
        public ObservableCollection<DishViewModel> Dishes
        {
            get { return dishes; }
            set { dishes = value; }
        }
        public ObservableCollection<FoodViewModel> Products
        {
            get { return products; }
            set { products = value; }
        }
        public ObservableCollection<FoodViewModel> Food
        {
            get { return food; }
            set { food = value; }
        }
        public FoodViewModel SelectedFood { get { return selectedFood; } set { SetValue<FoodViewModel>(ref selectedFood, value); } }
        public DishViewModel SelectedDish { get { return selectedDish; } set { SetValue<DishViewModel>(ref selectedDish, value); } }
        public FoodViewModel SelectedProduct { get { return selectedProduct; } set { SetValue<FoodViewModel>(ref selectedProduct, value); } }
        [CanExecute("CanReadyExecute")]
        public RelayCommand Ready { get { return ready; } }
        [CanExecute("CanAddProduct")]
        public RelayCommand AddProduct { get { return addProduct; } }
        [CanExecute("CanAddDish")]
        public RelayCommand AddDish { get { return addDish; } }
        [CanExecute("CanDeleteFood")]
        public RelayCommand Delete { get { return delete; } }
        public RelayCommand Cancel { get { return cancel; } }
        [DependentsUpon("SelectedProduct")]
        public bool CanAddProduct { get { return selectedProduct != null; } }
        [DependentsUpon("SelectedDish")]
        public bool CanAddDish { get { return selectedDish != null; } }
        [DependentsUpon("SelectedFood")]
        public bool CanDeleteFood { get { return selectedFood != null; } }
        public bool CanReadyExecute
        {
            get { return scheduleModel.Food.Count>0 || edit; }
        }
        public ScheduleInfoEditorViewModel(IWindowFactory windowFactory, IWindowController windowController, IDataManager dataManager, Schedule schedule, bool edit)
            : base(windowFactory,windowController)
        {
            selectedDayOfWeek = DateCharacteristic.GetDayOfWeek(schedule.DayOfWeek);
            selectedHour = schedule.Hour.ToString();
            selectedMinute = schedule.Minute.ToString();
            this.dataManager = dataManager;
            this.edit = edit;
            this.schedule = new ScheduleViewModel(schedule);
            scheduleModel = schedule;
            ready = new RelayCommand(ReadyExecute, _ => CanReadyExecute);
            cancel = new RelayCommand(CancelExecute);
            addProduct = new RelayCommand(AddProductExecute, _ => CanAddProduct);
            addDish = new RelayCommand(AddDishExecute, _ => CanAddDish);
            delete = new RelayCommand(DeleteExecute, _ => CanDeleteFood);
            dishes = new ObservableCollection<DishViewModel>();
            products = new ObservableCollection<FoodViewModel>();
            food = new ObservableCollection<FoodViewModel>();
            foreach(Dish d in dataManager.GetDishList())
            {
                dishes.Add(new DishViewModel(d));
            }
            foreach (Product p in dataManager.GetProductList())
            {
                products.Add(new FoodViewModel(p));
            }
            foreach (Food f in scheduleModel.Food)
            {
                food.Add(new FoodViewModel(f));
            }
        }
        private void AddProductExecute(object obj)
        {
            Product ourProduct = dataManager.GetProductByName(selectedProduct.Name);
            WindowFactory.CreateWeightEnterWindow(ourProduct);
            Food productInBase = schedule.Food.FirstOrDefault(p => p.Name == ourProduct.Name);
            if (productInBase != null)
            {
                CalculatorNutrients.CombineFood(productInBase, ourProduct);
                food[food.IndexOf(food.FirstOrDefault(p => p.Name == ourProduct.Name))] = new FoodViewModel(productInBase);
            }
            else
            {
                Food addingFood = new Food();
                addingFood.Carbohydrates = ourProduct.Carbohydrates;
                addingFood.Fats = ourProduct.Fats;
                addingFood.KiloCalories = ourProduct.KiloCalories;
                addingFood.Protein = ourProduct.Protein;
                addingFood.Weight = ourProduct.Weight;
                addingFood.Name = ourProduct.Name;
                scheduleModel.Food.Add(addingFood);
                food.Add(new FoodViewModel(ourProduct));
            }
            RaisePropertyChanged("CanReadyExecute");
        }        
        private void AddDishExecute(object obj)
        {
            Dish dish = dataManager.GetDishByName(selectedDish.Name);
            Food ourDish = new Food();
            ourDish.Carbohydrates = dish.Carbohydrates;
            ourDish.Fats = dish.Fats;
            ourDish.KiloCalories = dish.KiloCalories;
            ourDish.Protein = dish.Protein;
            ourDish.Weight = dish.Weight;
            ourDish.Name = dish.Name;
            WindowFactory.CreateWeightEnterWindow(ourDish);
            Food dishInBase = schedule.Food.FirstOrDefault(p => p.Name == ourDish.Name);
            if (dishInBase != null)
            {
                CalculatorNutrients.CombineFood(dishInBase, ourDish);
                food[food.IndexOf(food.FirstOrDefault(p => p.Name == ourDish.Name))] = new FoodViewModel(dishInBase);
            }
            else
            {
                scheduleModel.Food.Add(ourDish);
                food.Add(new FoodViewModel(ourDish));
            }
            RaisePropertyChanged("CanReadyExecute");
        }
        private void DeleteExecute(object obj)
        {
            scheduleModel.Food.Remove(scheduleModel.Food.FirstOrDefault(d => d.Name == selectedFood.Name));
            food.Remove(selectedFood);
            RaisePropertyChanged("CanReadyExecute");
        }
        private void ReadyExecute(object obj)
        {
            scheduleModel.DayOfWeek = DateCharacteristic.ParseDaysOfWeek(selectedDayOfWeek);
            scheduleModel.Hour = DateCharacteristic.ParseHourse(selectedHour);
            scheduleModel.Minute = DateCharacteristic.ParseMinute(selectedMinute);
            if (!edit)
            {
                dataManager.AddSchedule(scheduleModel);
            }
            else
            {
                dataManager.SetScheduleData(scheduleModel.UserId,scheduleModel.Id, scheduleModel);
            }
           WindowController.CloseWindow();
        }
        private void CancelExecute(object obj)
        {
            WindowController.CloseWindow();
        }
    }
}

