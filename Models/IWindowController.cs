using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public interface IWindowController
    {
        void CloseWindow();
        void ShutdownApplication();
    }
}
