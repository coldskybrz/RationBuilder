using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public class CalculatorNutrients
    {
        public static void RecalucalteFoodByWeight(IFood outputFood, float newWeight)
        {
            outputFood.Carbohydrates = (outputFood.Carbohydrates / outputFood.Weight) * newWeight;
            outputFood.Protein = (outputFood.Protein / outputFood.Weight) * newWeight;
            outputFood.Fats = (outputFood.Fats / outputFood.Weight) * newWeight;
            outputFood.KiloCalories = (outputFood.KiloCalories / outputFood.Weight) * newWeight;
            outputFood.Weight = newWeight;
        }
        public static void CombineFood(IFood outputFood, IFood inputFood)
        {
            outputFood.Carbohydrates += inputFood.Carbohydrates;
            outputFood.Protein += inputFood.Protein;
            outputFood.Fats += inputFood.Fats;
            outputFood.KiloCalories += inputFood.KiloCalories;
            outputFood.Weight += inputFood.Weight;
        }
        public static IFood CalculateNutrients(ICollection<IFood> products)
        {
            IFood calculatedFood = new Food();
            foreach (IFood nutrient in products)
            {
                calculatedFood.Protein += nutrient.Protein;
                calculatedFood.Carbohydrates += nutrient.Carbohydrates;
                calculatedFood.Fats += nutrient.Fats;
                calculatedFood.KiloCalories += nutrient.KiloCalories;
                calculatedFood.Weight += nutrient.Weight;
            }
            return calculatedFood;
        }
        public static IFood CalculateNutrients(ICollection<Schedule> schedules)
        {
            IFood calculatedFood = new Food();
            foreach (Schedule nutrient in schedules)
            {
                calculatedFood.Protein += nutrient.Protein;
                calculatedFood.Carbohydrates += nutrient.Carbohydrates;
                calculatedFood.Fats += nutrient.Fats;
                calculatedFood.KiloCalories += nutrient.KiloCalories;
                calculatedFood.Weight += nutrient.Weight;
            }
            return calculatedFood;
        }
        public static float CalculateProtein(ICollection<IFood> products)
        {
            return CalculateNutrients(products).Protein;
        }
        public static float CalculateCarbohydrates(ICollection<IFood> products)
        {
            return CalculateNutrients(products).Carbohydrates;
        }
        public static float CalculateFats(ICollection<IFood> products)
        {
            return CalculateNutrients(products).Fats;
        }
        public static float CalculateKiloCalories(ICollection<IFood> products)
        {
            return CalculateNutrients(products).KiloCalories;
        }
        public static float CalculateWeight(ICollection<IFood> products)
        {
            return CalculateNutrients(products).Weight;
        }
    }
}
