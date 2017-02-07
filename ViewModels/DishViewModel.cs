using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    public class DishViewModel : BaseViewModel
    {
        private IDish dish = null;
        public float Protein { get { return dish.Protein; } }
        public float Fats { get { return dish.Fats; } }
        public float Carbohydrates { get { return dish.Carbohydrates; }}
        public float KiloCalories { get { return dish.KiloCalories; }}
        public float Weight { get { return dish.Weight; }}
        public string Name { get { return dish.Name; } set { dish.Name = value; RaisePropertyChanged(); } }
        public List<Product> Products { get { return dish.List; } set { dish.List = value; RaisePropertyChanged(); } }
        public DishViewModel(IDish dish)
        {
            this.dish = dish;
        }
    }
}
