using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    public class WeightEnterViewModel : WindowControllableViewModel
    {
        private RelayCommand add = null;
        private float weight = 100;
        [CanExecute("CanReadyExecute")]
        public RelayCommand Add { get { return add; } }
        public float Weight { get { return weight; } set { SetValue<float>(ref weight, value); } }
        [DependentsUpon("Weight")]
        public bool CanReadyExecute { get { return weight > 0; } }
        public WeightEnterViewModel(IWindowController windowController, IFood food)
            : base(windowController)
        {
            weight = food.Weight;
            add = new RelayCommand(_ => { CalculatorNutrients.RecalucalteFoodByWeight(food, weight); WindowController.CloseWindow(); }, _ => CanReadyExecute);
        }
    }
}
