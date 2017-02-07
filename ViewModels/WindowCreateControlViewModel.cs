using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    public class WindowCreateControlViewModel : BaseViewModel
    {
        private IWindowFactory windowFactory = null;
        private IWindowController windowController = null;
        private RelayCommand closed = null;
        private bool canExecuteOnClosed = true;
        protected IWindowFactory WindowFactory
        {
            get { return windowFactory; }
        }
        protected IWindowController WindowController
        {
            get{return windowController;}
        }
        [CanExecute("CanExecuteOnClosed")]
        public RelayCommand Closed { get { return closed; } }
        public bool CanExecuteOnClosed
        {
            get { return canExecuteOnClosed; }
            set { SetValue<bool>(ref canExecuteOnClosed, value); }
        }
        protected virtual void OnClosed(object param) { }
        public WindowCreateControlViewModel(IWindowFactory windowFactory,IWindowController windowController)
        {
            this.windowFactory = windowFactory;
            this.windowController = windowController;
            closed = new RelayCommand(OnClosed, _ => CanExecuteOnClosed);
        }
    
    }
}
