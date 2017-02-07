using RationBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.ViewModels
{
    public class ScheduleViewModel : BaseViewModel
    {
        private Schedule schedule = null;
        public List<Food> Food { get { return schedule.Food; } set { schedule.Food = value; RaisePropertyChanged(); } }
        public DayOfWeek DayOfWeek { get { return schedule.DayOfWeek; } set { schedule.DayOfWeek = value; RaisePropertyChanged(); } }
        public string DayOfWeekFormated { get { return DateCharacteristic.GetDayOfWeek(DayOfWeek); } }
        public string Time { get { return Hour.ToString() + " : " + Minute.ToString(); } }
        public int Hour { get { return schedule.Hour; } set { schedule.Hour = value; RaisePropertyChanged(); } }
        public int Minute { get { return schedule.Minute; } set { schedule.Minute = value; RaisePropertyChanged(); } }
        public int Id { get { return schedule.Id; } set { schedule.Id = value; RaisePropertyChanged(); } }
        public int UserId { get { return schedule.UserId; } set { schedule.UserId = value; RaisePropertyChanged(); } }
        public float Protein { get { return schedule.Protein; } }
        public float Fats { get { return schedule.Fats; } }
        public float Carbohydrates { get { return schedule.Carbohydrates; } }
        public float KiloCalories { get { return schedule.KiloCalories; } }
        public float Weight { get { return schedule.Weight; } }
        public ScheduleViewModel(Schedule schedule)
        {
            this.schedule = schedule;
        }
    }
}
