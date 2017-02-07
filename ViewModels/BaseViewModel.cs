using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RationBuilder.Models;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RationBuilder.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RealizeDepeuntsUpon()
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
        private void RealizeCanExecute()
        {
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                List<CanExecute> dependentsAttributes = property.GetCustomAttributes<CanExecute>().ToList();
                foreach (CanExecute attribute in dependentsAttributes)
                {
                    this.PropertyChanged += new PropertyChangedEventHandler((obj, args) => { if (args.PropertyName == attribute.Name) { (property.GetValue(this) as RelayCommand).RaiseCanExecuteChanged(); } });
                }
            }
        }
        protected void SetValue<T>(ref T name, T value, [CallerMemberName] string propertyName = null)
        {
            if (name == null || !name.Equals(value))
            {
                name = value;
                RaisePropertyChanged(propertyName);
            }
        }
        protected void RaisePropertyChanged([CallerMemberName] string property = null)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }
        public BaseViewModel()
        {
            RealizeDepeuntsUpon();
            RealizeCanExecute();
        }
    }
    
}
