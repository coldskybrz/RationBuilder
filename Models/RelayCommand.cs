using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RationBuilder.Models
{
    public class RelayCommand : ICommand
    {
        
       private Action<object> execute;

       private Predicate<object> canExecute;

       public event EventHandler CanExecuteChanged;

       public RelayCommand(Action<object> execute)
           : this(execute, DefaultCanExecute)
       {
       }

       public RelayCommand(Action<object> execute, Predicate<object> canExecute)
       {
           if (execute == null)
           {
               throw new ArgumentNullException("execute");
           }

           if (canExecute == null)
           {
               throw new ArgumentNullException("canExecute");
           }

           this.execute = execute;
           this.canExecute = canExecute;
       }


       public bool CanExecute(object parameter)
       {
           return this.canExecute != null && this.canExecute(parameter);
       }

       public void Execute(object parameter)
       {
           this.execute(parameter);
       }

       public void RaiseCanExecuteChanged()
       {
           EventHandler handler = this.CanExecuteChanged;
           if (handler != null)
           {
               handler.Invoke(this, EventArgs.Empty);
           }
       }

       public void Destroy()
       {
           this.canExecute = _ => false;
           this.execute = _ => { return; };
       }

       private static bool DefaultCanExecute(object parameter)
       {
           return true;
       }
    }
}
