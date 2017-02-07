using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RationBuilder.Models
{
    [XmlRoot("Root")]
    public class Schedules
    {
        [XmlArrayItem("Schedule", typeof(Schedule))]
        [XmlArray("Schedules")]
        public List<Schedule> ScheduleList { get; set; }
        public Schedules()
        {
            ScheduleList = new List<Schedule>();
        }
        public Schedules(List<Schedule> Schedule)
        {
            ScheduleList = Schedule;
        }
    }
}
