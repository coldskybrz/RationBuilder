using Ninject;
using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RationBuilder
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        StandardKernel kernel;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
        }

        private void ConfigureContainer()
        {
            kernel = new StandardKernel();
            kernel.Bind<Application>().ToConstant(this).InSingletonScope();
            kernel.Bind<IDataManager>().To<DataManager>().InSingletonScope();
            kernel.Bind<IWindowFactory>().To<WindowFactory>().InSingletonScope();
            kernel.Bind<IRegistration>().To<Registration>().InSingletonScope();
            kernel.Bind<IAuthorization>().To<Authorization>().InSingletonScope();
        }

        private void ComposeObjects()
        {
            ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
            kernel.Get<WindowFactory>().CreateLoginWindow();   
        }
    }
}
