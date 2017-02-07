using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public class Schedule
    {
        public List<Food> Food { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Protein { get { return CalculatorNutrients.CalculateProtein(Food.ToArray<IFood>()); } }
        public float Fats { get { return CalculatorNutrients.CalculateFats(Food.ToArray<IFood>()); } }
        public float Carbohydrates { get { return CalculatorNutrients.CalculateCarbohydrates(Food.ToArray<IFood>()); } }
        public float KiloCalories { get { return CalculatorNutrients.CalculateKiloCalories(Food.ToArray<IFood>()); } }
        public float Weight { get { return CalculatorNutrients.CalculateWeight(Food.ToArray<IFood>()); } }
        public Schedule()
        {
            Food = new List<Food>();
        }
    }
}
