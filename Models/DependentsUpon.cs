using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
namespace RationBuilder.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DependentsUpon : Attribute
    {
        public string Name { get; set; }
        public DependentsUpon(string dependentOnProperty)
        {
            Name = dependentOnProperty;
        }
    }
}
