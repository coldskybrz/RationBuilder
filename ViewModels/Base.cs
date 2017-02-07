using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RationBuilder.Models;
using System.Reflection;
using System.ComponentModel;

namespace RationBuilder.ViewModels
{
    public class Base : NotifyPropertyChanged
    {
        public Base()
        {
            RealizePropertyDependent();
        }
        private void RealizePropertyDependent()
        {
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                List<DependentsUpon> dependentsAttributes = property.GetCustomAttributes<DependentsUpon>().ToList();
                foreach (DependentsUpon attribute in dependentsAttributes)
                {
                    this.PropertyChanged += new PropertyChangedEventHandler((obj, args) => { if (args.PropertyName == attribute.Name) { RaisePropertyChanged(property.Name); } });
                }
            }
        }
    }
}
