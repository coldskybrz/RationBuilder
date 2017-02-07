using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationBuilder.Models
{
    public class DateCharacteristic
    {
        private ObservableCollection<string> daysOfWeekItems = new ObservableCollection<string>();
        private ObservableCollection<string> minuteItems = new ObservableCollection<string>();
        private ObservableCollection<string> hourseItems = new ObservableCollection<string>();
        public ObservableCollection<string> DaysOfWeekitems { get { return daysOfWeekItems; } set { daysOfWeekItems = value; } }
        public ObservableCollection<string> MinuteItems { get { return minuteItems; } set { minuteItems = value; } }
        public ObservableCollection<string> HourseItems { get { return hourseItems; } set { hourseItems = value; } }
        public DateCharacteristic()
        {
            for(int i = 0;i < 60;i++)
            {
                minuteItems.Add(i.ToString());
            } 
            for (int i = 0; i < 24; i++)
            {
                hourseItems.Add(i.ToString());
            }
            daysOfWeekItems.Add(L10n.Resource.MONDAY);
            daysOfWeekItems.Add(L10n.Resource.TUESDAY);
            daysOfWeekItems.Add(L10n.Resource.WEDNESDAY);
            daysOfWeekItems.Add(L10n.Resource.THURSDAY);
            daysOfWeekItems.Add(L10n.Resource.FRIDAY);
            daysOfWeekItems.Add(L10n.Resource.SATURDAY);
            daysOfWeekItems.Add(L10n.Resource.SUNDAY);
        }
        public static string GetDayOfWeek(DayOfWeek day)
        {
            int indexOfDay = 0;
            switch (day)
            {
                case DayOfWeek.Tuesday:
                    indexOfDay = 1;
                    break;
                case DayOfWeek.Wednesday:
                    indexOfDay = 2;
                    break;
                case DayOfWeek.Thursday:
                    indexOfDay = 3;
                    break;
                case DayOfWeek.Friday:
                    indexOfDay = 4;
                    break;
                case DayOfWeek.Saturday:
                    indexOfDay = 5;
                    break;
                case DayOfWeek.Sunday:
                    indexOfDay = 6;
                    break;
            }
            return new DateCharacteristic().daysOfWeekItems[indexOfDay];
        }
        public static int ParseMinute(string minute)
        {
            return new DateCharacteristic().MinuteItems.IndexOf(minute);
        }
        public static int ParseHourse(string hourse)
        {
            return new DateCharacteristic().HourseItems.IndexOf(hourse);
        }
        public static DayOfWeek ParseDaysOfWeek(string week)
        {
            DayOfWeek day = DayOfWeek.Monday;
            switch(new DateCharacteristic().daysOfWeekItems.IndexOf(week))
            {
                case 1:
                    day = DayOfWeek.Tuesday;
                    break;
                case 2:
                    day = DayOfWeek.Wednesday;
                    break;
                case 3:
                    day = DayOfWeek.Thursday;
                    break;
                case 4:
                    day = DayOfWeek.Friday;
                    break;
                case 5:
                    day = DayOfWeek.Saturday;
                    break;
                case 6:
                    day = DayOfWeek.Sunday;
                    break;
            }
            return day;
        }
    }
}
