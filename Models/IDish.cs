using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public interface IDish
    {
        List<Product> List { get; set; }
        float Protein { get; }
        float Fats { get; }
        float Carbohydrates { get; }
        float KiloCalories { get; }
        float Weight { get; }
        string Name { get; set; }
    }
}
