using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    public class DishInfoEditorViewModel : WindowCreateControlViewModel
    {
        private IDataManager dataManager = null;
        private RelayCommand ready = null;
        private RelayCommand cancel = null;
        private RelayCommand add = null;
        private RelayCommand delete = null;
        private DishViewModel dish = null;
        private FoodViewModel selectedDishProduct = null;
        private FoodViewModel selectedProduct = null;
        private ObservableCollection<FoodViewModel> products = null;
        private ObservableCollection<FoodViewModel> dishProducts = null;
        private string name = null;
        private bool edit = false;
        private Dish dishModel = null;
        public DishViewModel Dish 
        {
            get { return dish; } 
            set 
            {
                SetValue<DishViewModel>(ref dish, value);
            }
        }
        public ObservableCollection<FoodViewModel> DishProducts
        {
            get { return dishProducts; }
            set { dishProducts = value; }
        }
        public ObservableCollection<FoodViewModel> Products
        {
            get { return products; }
            set { products = value; }
        }
        public FoodViewModel SelectedDishProduct { get { return selectedDishProduct; } set { SetValue<FoodViewModel>(ref selectedDishProduct, value); } }
        public FoodViewModel SelectedProduct { get { return selectedProduct; } set { SetValue<FoodViewModel>(ref selectedProduct, value); } }
        public string Name
        {
            set
            {
                Dish.Name = value;
                RaisePropertyChanged();
            }
            get { return Dish.Name; }
        }
        [CanExecute("CanReadyExecute")]
        public RelayCommand Ready { get { return ready; } }
        [CanExecute("CanAddProduct")]
        public RelayCommand Add { get { return add; } }
        [CanExecute("CanDeleteProduct")]
        public RelayCommand Delete { get { return delete; } }
        public RelayCommand Cancel { get { return cancel; } }
        [DependentsUpon("SelectedProduct")]
        public bool CanAddProduct { get { return selectedProduct != null; } }
        [DependentsUpon("SelectedDishProduct")]
        public bool CanDeleteProduct { get { return selectedDishProduct != null; } }
        [DependentsUpon("Name")]
        [DependentsUpon("Dish")]
        public bool CanReadyExecute
        {
            get { return dish.Name != null && (dataManager.GetProductByName(dish.Name) == null || edit) && Dish.Products.Count>0; }
        }
        public DishInfoEditorViewModel(IWindowFactory windowFactory, IWindowController windowController, IDataManager dataManager, Dish dish, bool edit)
            : base(windowFactory,windowController)
        {
            this.dataManager = dataManager;
            this.edit = edit;
            this.dish = new DishViewModel(dish);
            dishModel = dish;
            this.name = dish.Name;
            ready = new RelayCommand(ReadyExecute, _ => CanReadyExecute);
            cancel = new RelayCommand(CancelExecute);
            add = new RelayCommand(AddExecute, _ => CanAddProduct);
            delete = new RelayCommand(DeleteExecute, _ => CanDeleteProduct);
            products = new ObservableCollection<FoodViewModel>();
            foreach(Product p in dataManager.GetProductList())
            {
                products.Add(new FoodViewModel(p));
            }
            dishProducts = new ObservableCollection<FoodViewModel>();
            foreach (Product p in Dish.Products)
            {
                dishProducts.Add(new FoodViewModel(p));
            }
        }
        private void AddExecute(object obj)
        {
            Product ourProduct = dataManager.GetProductByName(selectedProduct.Name);
            WindowFactory.CreateWeightEnterWindow(ourProduct);
            Product productInBase = dishModel.List.FirstOrDefault(p => p.Name == ourProduct.Name);
            if (productInBase != null)
            {
                CalculatorNutrients.CombineFood(productInBase, ourProduct);
                DishProducts[DishProducts.IndexOf(DishProducts.FirstOrDefault(p => p.Name == ourProduct.Name))] = new FoodViewModel(productInBase);
            }
            else
            {
                dishModel.List.Add(ourProduct);
                dishProducts.Add(new FoodViewModel(ourProduct));
            }
            RaisePropertyChanged("Dish");
        }
        private void DeleteExecute(object obj)
        {
            dishModel.List.Remove(dishModel.List.FirstOrDefault(d => d.Name == selectedDishProduct.Name));
            dishProducts.Remove(selectedDishProduct);
            RaisePropertyChanged("Dish");
        }
        private void ReadyExecute(object obj)
        {
            if (!edit)
            {
                dataManager.AddDish(dishModel);
            }
            else
            {
                dataManager.SetDishData(name, dishModel);
            }
           WindowController.CloseWindow();
        }
        private void CancelExecute(object obj)
        {
            WindowController.CloseWindow();
        }
    }
}
