using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    public class FoodViewModel : BaseViewModel
    {
        private IFood food = null;
        public float Protein { get { return food.Protein; } set { food.Protein = value; RaisePropertyChanged(); } }
        public float Fats { get { return food.Fats; } set { food.Fats = value; RaisePropertyChanged(); } }
        public float Carbohydrates { get { return food.Carbohydrates; } set { food.Carbohydrates = value; RaisePropertyChanged(); } }
        public float KiloCalories { get { return food.KiloCalories; } set { food.KiloCalories = value; RaisePropertyChanged(); } }
        public float Weight { get { return food.Weight; } set { food.Weight = value; RaisePropertyChanged(); } }
        public string Name { get { return food.Name; } set { food.Name = value; RaisePropertyChanged(); } }
        public FoodViewModel(IFood food)
        {
            this.food = food;
        }
    }
}
