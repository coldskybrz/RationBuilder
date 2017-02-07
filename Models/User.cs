using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public class User : IUser
    {
        public Gender Gender { get; set; }
        public DailyActivity DailyActivity { get; set; }
        public PurposeActivity PurposeActivity { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public User()
        {

        }
    }
}
