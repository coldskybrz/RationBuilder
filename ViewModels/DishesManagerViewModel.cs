using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    class DishesManagerViewModel : WindowCreateControlViewModel
    {
        private IDataManager dataManager = null;
        private RelayCommand add = null;
        private RelayCommand edit = null;
        private RelayCommand delete = null;
        private ObservableCollection<DishViewModel> dishes = null;
        private DishViewModel selectedDish = null;
        private void LoadDishesList()
        {
            dishes.Clear();
            foreach(Dish f in dataManager.GetDishList())
            {
                dishes.Add(new DishViewModel(f));
            }
        }
        public ObservableCollection<DishViewModel> Dishes { get { return dishes; } set { dishes = value; } }
        [DependentsUpon("SelectedDish")]
        public ObservableCollection<FoodViewModel> Products
        {
            get
            {
                ObservableCollection<FoodViewModel> products = new ObservableCollection<FoodViewModel>();
                if (selectedDish != null)
                {
                    foreach (Product p in selectedDish.Products)
                    {
                        products.Add(new FoodViewModel(p));
                    }
                }
                return products;
            }
        }
        public DishViewModel SelectedDish { get { return selectedDish; } set { SetValue<DishViewModel>(ref selectedDish, value); } }
        public RelayCommand Add { get { return add; } }
        [CanExecute("CanEditOrDelete")]
        public RelayCommand Edit { get { return edit; } }
        [CanExecute("CanEditOrDelete")]
        public RelayCommand Delete { get { return delete; } }
        [DependentsUpon("SelectedDish")]
        public bool CanEditOrDelete { get { return selectedDish != null; } }
        public DishesManagerViewModel(IWindowFactory windowFactory, IWindowController windowController, IDataManager dataManager)
            : base(windowFactory, windowController)
        {
            dishes = new ObservableCollection<DishViewModel>();
            this.dataManager = dataManager;
            LoadDishesList();
            add = new RelayCommand(_ =>
            {
                windowFactory.CreateDishInfoEditorWindow(new Dish(), false);
                LoadDishesList();
            });
            edit = new RelayCommand(_ =>
            {
                windowFactory.CreateDishInfoEditorWindow(dataManager.GetDishByName(selectedDish.Name), true);
                LoadDishesList();
            }, _ => CanEditOrDelete);
            delete = new RelayCommand(_ =>
            {
                dataManager.RemoveDish(selectedDish.Name);
                LoadDishesList();
            }, _ => CanEditOrDelete);
        }
    }
}
