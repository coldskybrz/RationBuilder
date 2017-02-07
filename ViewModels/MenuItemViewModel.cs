using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    public class MenuItemViewModel : BaseViewModel
    {
        private ObservableCollection<MenuItemViewModel> items = new ObservableCollection<MenuItemViewModel>();
        private string title = null;
        public ObservableCollection<MenuItemViewModel> Items { get { return items; } set { items = value; } }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                SetValue<string>(ref title, value);
            }
        }
        public MenuItemViewModel()
        { }
        public MenuItemViewModel(ObservableCollection<MenuItemViewModel> list, string title)
        {
            Items = list;
            Title = title;
        }
    }
}
