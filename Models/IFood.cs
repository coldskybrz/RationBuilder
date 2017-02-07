using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public interface IFood
    {
        float Protein { get; set; }
        float Fats { get; set; }
        float Carbohydrates { get; set; }
        float KiloCalories { get; set; }
        float Weight { get; set; }
        string Name { get; set; }
    }
}
