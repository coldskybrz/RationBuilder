using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    public class WindowControllableViewModel : BaseViewModel
    {
        private IWindowController windowController = null;
        private RelayCommand closed = null;
        private bool canExecuteOnClosed = true;
        public bool CanExecuteOnClosed
        {
            get { return canExecuteOnClosed; }
            set { SetValue<bool>(ref canExecuteOnClosed, value); }
        }
        [CanExecute("CanExecuteOnClosed")]
        public RelayCommand Closed { get { return closed; } }
        protected IWindowController WindowController
        {
            get
            {
                return windowController;
            }
        }
        protected virtual void OnClosed(object param) { }
        public WindowControllableViewModel(IWindowController windowController)
        {
            closed = new RelayCommand(OnClosed, _ => CanExecuteOnClosed);
            this.windowController = windowController;
        }
    }
}
