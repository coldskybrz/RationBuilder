using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CanExecute : Attribute
    {
        public string Name { get; set; }
        public CanExecute(string canExecuteName)
        {
            Name = canExecuteName;
        }
    }
}
