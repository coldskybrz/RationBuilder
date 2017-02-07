using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public interface IUser
    {
        string Login { get; set; }
        string Password { get; set; }
        int Age { get; set; }
        int Height { get; set; }
        int Weight { get; set; }
        int Id { get; set; }
        DailyActivity DailyActivity { get; set; }
        PurposeActivity PurposeActivity { get; set; }
        Gender Gender { get; set; }
    }
}
