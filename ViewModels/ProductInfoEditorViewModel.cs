using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    public class ProductInfoEditorViewModel : WindowControllableViewModel
    {
        private IDataManager dataManager = null;
        private RelayCommand ready = null;
        private RelayCommand cancel = null;
        private FoodViewModel product = null;
        private string name = null;
        private bool edit = false;
        private Product productModel = null;
        public FoodViewModel Product 
        {
            get { return product; } 
            set 
            {
                SetValue<FoodViewModel>(ref product, value);
            }
        }
        public string Name
        {
            set
            {
                Product.Name = value;
                RaisePropertyChanged();
            }
            get { return Product.Name; }
        }
        public float Weight
        {
            get { return Product.Weight; }
            set { Product.Weight = value; RaisePropertyChanged(); }
        }
        [CanExecute("CanReadyExecute")]
        public RelayCommand Ready { get { return ready; } }
        public RelayCommand Cancel { get { return cancel; } }
        [DependentsUpon("Name")]
        [DependentsUpon("Weight")]
        public bool CanReadyExecute
        {
            get { return product.Name != null && (dataManager.GetProductByName(product.Name) == null && dataManager.GetDishByName(product.Name) == null || edit) && product.Weight>0;}
        }
        public ProductInfoEditorViewModel(IWindowController windowController, IDataManager dataManager, Product product, bool edit)
            : base(windowController)
        {
            this.dataManager = dataManager;
            this.edit = edit;
            this.product = new FoodViewModel(product);
            productModel = product;
            this.name = product.Name;
            ready = new RelayCommand(ReadyExecute, _ => CanReadyExecute);
            cancel = new RelayCommand(CancelExecute);
        }
        private void ReadyExecute(object obj)
        {
            if (!edit)
            {
                dataManager.AddProduct(productModel);
            }
            else
            {
                dataManager.SetProductData(name, productModel);
            }
           WindowController.CloseWindow();
        }
        private void CancelExecute(object obj)
        {
            WindowController.CloseWindow();
        }
    }
}
