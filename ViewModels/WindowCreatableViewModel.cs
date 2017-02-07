using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    public class WindowCreatableViewModel : BaseViewModel
    {
        private IWindowFactory windowFactory = null;
        protected IWindowFactory WindowFactory
        {
            get { return windowFactory; }
        }
        public WindowCreatableViewModel(IWindowFactory windowFactory)
        {
            this.windowFactory = windowFactory;
        }
    }
}
