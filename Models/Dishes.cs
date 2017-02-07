using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RationBuilder.Models
{
    [XmlRoot("Root")]
    public class Dishes
    {
        [XmlArrayItem("Dish", typeof(Dish))]
        [XmlArray("Dishes")]
        public List<Dish> DishList { get; set; }
        public Dishes()
        {
            DishList = new List<Dish>();
        }
        public Dishes(List<Dish> dishes)
        {
            DishList = dishes;
        }
    }
}
