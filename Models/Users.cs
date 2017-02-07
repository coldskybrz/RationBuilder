using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RationBuilder.Models
{
    [XmlRoot("Root")]
    public class Users
    {
        [XmlArrayItem("User", typeof(User))]
        [XmlArray("Users")]
        public List<User> UserList { get; set; }
        public Users()
        {
            UserList = new List<User>();
        }
        public Users(List<User> users)
        {
            UserList = users;
        }
    }
}
