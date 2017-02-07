using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RationBuilder.Models
{
    [XmlRoot("Root")]
    public class Products
    {
        [XmlArrayItem("Product", typeof(Product))]
        [XmlArray("Products")]
        public List<Product> ProductList { get; set; }
        public Products()
        {
            ProductList = new List<Product>();
        }
        public Products(List<Product> products)
        {
            ProductList = products;
        }
    }
}
