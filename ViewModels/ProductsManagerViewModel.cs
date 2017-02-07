using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using RationBuilder.Models;

namespace RationBuilder.ViewModels
{
    public class ProductsManagerViewModel : WindowCreateControlViewModel
    {
        private IDataManager dataManager = null;
        private RelayCommand add = null;
        private RelayCommand edit = null;
        private RelayCommand delete = null;
        private ObservableCollection<FoodViewModel> products = null;
        private FoodViewModel selectedProduct = null;
        private void LoadProductsList()
        {
            products.Clear();
            foreach(Product f in dataManager.GetProductList())
            {
                products.Add(new FoodViewModel(f));
            }
        }
        public ObservableCollection<FoodViewModel> Products { get { return products; } set { products = value; } }
        public FoodViewModel SelectedProduct { get { return selectedProduct; } set { SetValue<FoodViewModel>(ref selectedProduct, value); } }
        public RelayCommand Add { get { return add; } }
        [CanExecute("CanEditOrDelete")]
        public RelayCommand Edit { get { return edit; } }
        [CanExecute("CanEditOrDelete")]
        public RelayCommand Delete { get { return delete; } }
        [DependentsUpon("SelectedProduct")]
        public bool CanEditOrDelete { get { return selectedProduct != null; } }
        public ProductsManagerViewModel(IWindowFactory windowFactory, IWindowController windowController, IDataManager dataManager)
            : base(windowFactory, windowController)
        {
            products = new ObservableCollection<FoodViewModel>();
            this.dataManager = dataManager;
            LoadProductsList();
            add = new RelayCommand(_ =>
            {
                windowFactory.CreateProductInfoEditorWindow(new Product(), false);
                LoadProductsList();
            });
            edit = new RelayCommand(_ =>
            {
                windowFactory.CreateProductInfoEditorWindow(dataManager.GetProductByName(selectedProduct.Name), true);
                LoadProductsList();
            }, _ => CanEditOrDelete);
            delete = new RelayCommand(_ =>
            {
                dataManager.RemoveProduct(selectedProduct.Name);
                LoadProductsList();
            }, _ => CanEditOrDelete);
        }
    }
}
