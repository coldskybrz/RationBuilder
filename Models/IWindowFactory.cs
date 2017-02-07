using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public interface IWindowFactory
    {
        void CreateLoginWindow();
        void CreateRationWindow(int id);
        void CreateRegisterWindow();
        void CreateUserEditWindow(int id);
        void CreateProductsManagerWindow();
        void CreateProductInfoEditorWindow(Product product, bool edit);
        void CreateDishesManagerWindow();
        void CreateDishInfoEditorWindow(Dish dish, bool edit);
        void CreateWeightEnterWindow(IFood food);
        void CreateScheduleManagerWindow(int userId);
        void CreateScheduleInfoEditorWindow(Schedule schedule, bool edit);
    }
}
