using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public interface IDataManager
    {
        void AddUser(User user);
        void AddProduct(Product product);
        void AddDish(Dish dish);
        void AddSchedule(Schedule schedule);
        void RemoveUser(int id);
        void RemoveProduct(string name);
        void RemoveDish(string name);
        void RemoveSchedule(int userId, int id);
        IEnumerable<User> GetUserList();
        IEnumerable<Dish> GetDishList();
        IEnumerable<Product> GetProductList();
        IEnumerable<Schedule> GetScheduleList(int userId);
        User GetUser(int id);
        Product GetProductByName(string name);
        Dish GetDishByName(string name);
        Gender GetUserGender(int id);
        Schedule GetScheduleById(int userId, int id);
        DailyActivity GetUserDailyActivity(int id);
        PurposeActivity GetUserPurposeActivity(int id);
        int GetUserHeight(int id);
        int GetUserWeight(int id);
        int GetIdByLogin(string login);
        string GetUserLogin(int id);
        string GetUserPassword(int id);
        int GetUserAge(int id);
        void SetProductData(string name, Product product);
        void SetDishData(string name, Dish dish);
        void SetUserData(int id, User user);
        void SetUserGender(int id, Gender gender);
        void SetScheduleData(int userId, int id, Schedule schedule);
        void SetUserDailyActivity(int id, DailyActivity dailyActivity);
        void SetUserPurposeActivity(int id, PurposeActivity purposeActivity);
        void SetUserHeight(int id, int height);
        void SetUserWeight(int id, int weight);
        void SetUserAge(int id, int age);
    }
}
