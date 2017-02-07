using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public class Dish : IDish
    {
        public List<Product> List { get; set; }
        public float Protein { get { return CalculatorNutrients.CalculateProtein(List.ToArray<IFood>()); } }
        public float Fats { get { return CalculatorNutrients.CalculateFats(List.ToArray<IFood>()); } }
        public float Carbohydrates { get { return CalculatorNutrients.CalculateCarbohydrates(List.ToArray<IFood>()); } }
        public float KiloCalories { get { return CalculatorNutrients.CalculateKiloCalories(List.ToArray<IFood>()); } }
        public float Weight { get { return CalculatorNutrients.CalculateWeight(List.ToArray<IFood>()); } }
        public string Name { get; set; }
        public Dish()
        {
            List = new List<Product>();
        }
    }
}
