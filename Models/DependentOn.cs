using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
namespace RationBuilder.Models
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = true)]
    public class DependentOn : Attribute
    {
        private event PropertyChangedEventHandler PropertyChanged;
        private string propertyChangedName;
        private string raisingPropertyChanged;
        public DependentOn(string propertyChangedName, PropertyChangedEventHandler PropertyChanged, [CallerMemberName] string ourProperty = null)
        {
            this.PropertyChanged = PropertyChanged;
            this.propertyChangedName = propertyChangedName;
            this.raisingPropertyChanged = ourProperty;
        }
        private void PropertyChangedEvent(object obj, PropertyChangedEventArgs args)
        {
            if(propertyChangedName == args.PropertyName)
            {
                var pChanged = PropertyChanged;
                if (pChanged != null)
                {
                    pChanged.Invoke(this, new PropertyChangedEventArgs(raisingPropertyChanged));
                }
            }
        }
    }
}
