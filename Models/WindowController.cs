using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RationBuilder.Models
{
    public class WindowController : IWindowController
    {
        private Window window = null;
        private Application app = null;
        public WindowController(Window window, Application app)
        {
            this.window = window;
            this.app = app;
        }
        public void CloseWindow()
        {
            window.Close();
        }
        public void ShutdownApplication()
        {
            app.Shutdown();
        }
    }
}
