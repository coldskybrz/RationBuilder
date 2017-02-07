using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public class Food : IFood
    {
        public float Protein { get; set; }
        public float Fats { get; set; }
        public float Carbohydrates { get; set; }
        public float KiloCalories { get; set; }
        public float Weight { get; set; }
        public string Name { get; set; }
    }
}
