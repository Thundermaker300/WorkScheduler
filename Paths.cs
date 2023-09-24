using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkScheduler
{
    public static class Paths
    {
        public static readonly string MainDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ScheduleData");
        public static readonly string PeopleJson = Path.Combine(MainDirectory, "employees.json");
        public static readonly string SchedulesFolder = Path.Combine(MainDirectory, "Schedules");
    }
}
