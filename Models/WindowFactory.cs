using RationBuilder.ViewModels;
using RationBuilder.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RationBuilder.Models
{
    public class WindowFactory : IWindowFactory
    {
        private Application app = null;
        private IDataManager dataManager = null;
        private IAuthorization authorization = null;
        private IRegistration registration = null;
        protected WindowController NewWindowController(Window window)
        {
            return new WindowController(window, app);
        }
        public WindowFactory(Application app, IDataManager dataManager, IAuthorization authorization, IRegistration registration)
        {
            this.app = app;
            this.dataManager = dataManager;
            this.authorization = authorization;
            this.registration = registration;
        }
        public void CreateLoginWindow()
        {
            LoginWindow window = new LoginWindow();
            window.DataContext = new LoginViewModel(this, NewWindowController(window), authorization);
            window.Show();
        }
        public void CreateRationWindow(int id)
        {
            RationWindow window = new RationWindow();
            window.DataContext = new RationViewModel(this, NewWindowController(window),dataManager,id);
            window.Show();
        }
        public void CreateRegisterWindow()
        {
            RegisterWindow window = new RegisterWindow();
            window.DataContext = new RegistrationViewModel(this, NewWindowController(window), registration);
            window.ShowDialog();
        }
        public void CreateUserEditWindow(int id)
        {
            UserEditWindow window = new UserEditWindow();
            window.DataContext = new UserEditViewModel(NewWindowController(window), dataManager, id);
            window.ShowDialog();
        }
        public void CreateProductsManagerWindow()
        {
            ProductsManagerWindow window = new ProductsManagerWindow();
            window.DataContext = new ProductsManagerViewModel(this, NewWindowController(window), dataManager);
            window.ShowDialog();
        }
        public void CreateProductInfoEditorWindow(Product product, bool edit)
        {
            ProductInfoEditorWindow window = new ProductInfoEditorWindow();
            window.DataContext = new ProductInfoEditorViewModel(NewWindowController(window), dataManager, product, edit);
            window.ShowDialog();
        }
        public void CreateDishesManagerWindow()
        {
            DishesManagerWindow window = new DishesManagerWindow();
            window.DataContext = new DishesManagerViewModel(this, NewWindowController(window), dataManager);
            window.ShowDialog();
        }
        public void CreateDishInfoEditorWindow(Dish dish, bool edit)
        {
            DishInfoEditorWindow window = new DishInfoEditorWindow();
            window.DataContext = new DishInfoEditorViewModel(this,NewWindowController(window), dataManager, dish, edit);
            window.ShowDialog();
        }
        public void CreateWeightEnterWindow(IFood food)
        {
            WeightEnterWindow window = new WeightEnterWindow();
            window.DataContext = new WeightEnterViewModel(NewWindowController(window), food);
            window.ShowDialog();
        }
        public void CreateScheduleManagerWindow(int userId)
        {
            ScheduleManagerWindow window = new ScheduleManagerWindow();
            window.DataContext = new ScheduleManagerViewModel(this, NewWindowController(window), dataManager, userId);
            window.ShowDialog();
        }
        public void CreateScheduleInfoEditorWindow(Schedule schedule, bool edit)
        {
            ScheduleInfoEditorWindow window = new ScheduleInfoEditorWindow();
            window.DataContext = new ScheduleInfoEditorViewModel(this, NewWindowController(window), dataManager, schedule, edit);
            window.ShowDialog();
        }
    }
}
