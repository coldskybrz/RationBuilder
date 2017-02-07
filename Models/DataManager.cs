using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RationBuilder.Models
{
    public class DataManager : IDataManager
    {
        private string usersPath = @"data\users.xml";
        private string productsPath = @"data\products.xml";
        private string dishesPath = @"data\dishes.xml";
        private string schedulesPath = @"data\schedules.xml";
        private string dataPath = @"data";
        private void Serialize<T>(T type, string path) where T : class
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (FileStream file = File.Create(path))
            {
                xmlSerializer.Serialize(file, type);
            }
        }
        private T Deserialize<T>(string path) where T : class
        {
            XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
            T returnObject = null;
            using (FileStream file = File.OpenRead(path))
            {
                if (file.Length > 0)
                {
                    returnObject = (xmlSerializer.Deserialize(file) as T);
                }
            }
            return returnObject;
        }

        private bool ExistObject<ObjectType>(IEnumerable<ObjectType> array, Predicate<ObjectType> predicate)
        {
            return array.ToList<ObjectType>().Exists(predicate);
        }
        private ObjectType FindObject<ObjectType>(IEnumerable<ObjectType> array, Predicate<ObjectType> predicate)
        {
            return array.ToList<ObjectType>().Find(predicate);
        }
        private void CreateFileIfNotExist(string path)
        {
            if(!File.Exists(path))
            {
                FileStream file = File.Create(path);
                file.Close();
            }
        }
        private DeserializeType GetListByDeserialize<DeserializeType>(string path) where DeserializeType : class, new()
        {
            DeserializeType array = Deserialize<DeserializeType>(path);
            if (array == null)
            {
                array = new DeserializeType();
            }
            return array;
        }
        private User FindUserById(int id)
        {
            return GetUserList().ToList().Find(u => u.Id == id);
        }
        public DataManager()
        {
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            CreateFileIfNotExist(usersPath);
            CreateFileIfNotExist(productsPath);
            CreateFileIfNotExist(dishesPath);
            CreateFileIfNotExist(schedulesPath);
        }
        public void AddUser(User user)
        {
            IEnumerable<User> array = GetUserList();
            Users users = new Users(array == null ? new List<User>() : array.ToList());
            users.UserList.Add(user);
            Serialize<Users>(users, usersPath);
        }
        public void AddProduct(Product product)
        {
            IEnumerable<Product> array = GetProductList();
            Products products = new Products(array == null ? new List<Product>() : array.ToList());
            products.ProductList.Add(product);
            Serialize<Products>(products, productsPath);
        }
        public void AddDish(Dish dish)
        {
            IEnumerable<Dish> array = GetDishList();
            Dishes dishes = new Dishes(array == null ? new List<Dish>() : array.ToList());
            dishes.DishList.Add(dish);
            Serialize<Dishes>(dishes, dishesPath);
        }
        public void AddSchedule(Schedule schedule)
        {
            IEnumerable<Schedule> array = GetScheduleList(schedule.UserId);
            Schedules dishes = new Schedules(array == null ? new List<Schedule>() : array.ToList());
            schedule.Id = dishes.ScheduleList.Count;
            dishes.ScheduleList.Add(schedule);
            Serialize<Schedules>(dishes, schedulesPath);
        }
        public void RemoveUser(int id)
        {
            Users users = new Users(GetUserList().ToList());
            users.UserList.Remove(users.UserList.Find(u => u.Id == id));
            Serialize<Users>(users, usersPath);
        }
        public void RemoveSchedule(int userId, int id)
        {
            Schedules schedules = new Schedules(GetScheduleList(userId).ToList());
            schedules.ScheduleList.Remove(schedules.ScheduleList.Find(s => s.Id == id));
            Serialize<Schedules>(schedules, schedulesPath);
        }
        public void RemoveProduct(string name)
        {
            Products products = new Products(GetProductList().ToList());
            products.ProductList.Remove(products.ProductList.Find(p => p.Name == name));
            Serialize<Products>(products, productsPath);
        }
        public void RemoveDish(string name)
        {
            Dishes dishes = new Dishes(GetDishList().ToList());
            dishes.DishList.Remove(dishes.DishList.Find(p => p.Name == name));
            Serialize<Dishes>(dishes, dishesPath);
        }
        public IEnumerable<User> GetUserList()
        {
            return GetListByDeserialize<Users>(usersPath).UserList;
        }
        public IEnumerable<Dish> GetDishList()
        {
            return GetListByDeserialize<Dishes>(dishesPath).DishList;
        }
        public IEnumerable<Product> GetProductList()
        {
            return GetListByDeserialize<Products>(productsPath).ProductList;
        }
        public IEnumerable<Schedule> GetScheduleList(int userId)
        {
            List<Schedule> schedules = GetListByDeserialize<Schedules>(schedulesPath).ScheduleList;
            List<Schedule> schedulesUser = new List<Schedule>();
            foreach(Schedule s in schedules)
            {
                if(s.UserId == userId)
                {
                    schedulesUser.Add(s);
                }
            }
            return schedulesUser;
        }

        public User GetUser(int id)
        {
            return FindObject<User>(GetUserList(), u => u.Id == id);
        }
        public Product GetProductByName(string name)
        {
            Product product = null;
            if(ExistObject<Product>(GetProductList(), p => p.Name == name))
            {
                product = FindObject<Product>(GetProductList(), p => p.Name == name);
            }
            return product;
        }
        public Dish GetDishByName(string name)
        {
            Dish product = null;
            if (ExistObject<Dish>(GetDishList(), d => d.Name == name))
            {
                product = FindObject<Dish>(GetDishList(), d => d.Name == name);
            }
            return product;
        }
        public Schedule GetScheduleById(int userId, int id)
        {
            Schedule schedule = null;
            if (ExistObject<Schedule>(GetScheduleList(userId), s => s.Id == id))
            {
                schedule = FindObject<Schedule>(GetScheduleList(userId), s => s.Id == id);
            }
            return schedule;
        }
        public Gender GetUserGender(int id)
        {
            return FindObject<User>(GetUserList(), u => u.Id == id).Gender;
        }
        public DailyActivity GetUserDailyActivity(int id)
        {
            return FindObject<User>(GetUserList(), u => u.Id == id).DailyActivity;
        }
        public PurposeActivity GetUserPurposeActivity(int id)
        {
            return FindObject<User>(GetUserList(), u => u.Id == id).PurposeActivity;
        }
        public int GetUserHeight(int id)
        {
            return FindObject<User>(GetUserList(), u => u.Id == id).Height;
        }
        public int GetUserWeight(int id)
        {
            return FindObject<User>(GetUserList(), u => u.Id == id).Weight;
        }
        public int GetIdByLogin(string login)
        {
            int id = -1;
            if (ExistObject<User>(GetUserList(), u => u.Login == login))
            {
                id = FindObject<User>(GetUserList(), u => u.Login == login).Id;
            }
            return id;
        }
        public string GetUserLogin(int id)
        {
           return FindObject<User>(GetUserList(), u => u.Id == id).Login;
        }
        public string GetUserPassword(int id)
        {
            return FindObject<User>(GetUserList(), u => u.Id == id).Password;
        }
        public int GetUserAge(int id)
        {
            return FindObject<User>(GetUserList(), u => u.Id == id).Age;
        }
        public void SetProductData(string name, Product product)
        {
            List<Product> products = GetProductList().ToList();
            products[products.FindIndex(d => d.Name == name)] = product;
            Serialize<Products>(new Products(products), productsPath);
        }
        public void SetDishData(string name, Dish dish)
        {
            List<Dish> dishes = GetDishList().ToList();
            dishes[dishes.FindIndex(d => d.Name == name)] = dish;
            Serialize<Dishes>(new Dishes(dishes), dishesPath);
        }
        public void SetUserData(int id, User user)
        {
            List<User> users = GetUserList().ToList<User>();
            users[users.FindIndex(u => u.Id == id)] = user;
            Serialize<Users>(new Users(users), usersPath);
        }
        public void SetUserGender(int id, Gender gender)
        {
            User user = FindUserById(id);
            user.Gender = gender;
            SetUserData(id, user);
        }
        public void SetUserDailyActivity(int id, DailyActivity dailyActivity)
        {
            User user = FindUserById(id);
            user.DailyActivity = dailyActivity;
            SetUserData(id, user);
        }
        public void SetUserPurposeActivity(int id, PurposeActivity purposeActivity)
        {
            User user = FindUserById(id);
            user.PurposeActivity = purposeActivity;
            SetUserData(id, user);
        }
        public void SetUserHeight(int id, int height)
        {
            User user = FindUserById(id);
            user.Height = height;
            SetUserData(id, user);
        }
        public void SetUserWeight(int id, int weight)
        {
            User user = FindUserById(id);
            user.Weight = weight;
            SetUserData(id, user);
        }
        public void SetUserAge(int id, int age)
        {
            User user = FindUserById(id);
            user.Age = age;
            SetUserData(id, user);
        }
        public void SetScheduleData(int userId, int id, Schedule schedule)
        {
            List<Schedule> schedules = GetScheduleList(userId).ToList<Schedule>();
            schedules[schedules.FindIndex(u => u.Id == id)] = schedule;
            Serialize<Schedules>(new Schedules(schedules), schedulesPath);
        }
    }
}
