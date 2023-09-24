using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WorkScheduler.Structures
{
    public class Schedule
    {
        public Schedule(string weekOf)
        {
            WeekOf = weekOf;
            TheSchedule = new();
        }

        public string WeekOf { get; }
        public Dictionary<Days, Dictionary<string, string>> TheSchedule { get; }

        public bool Add(Person p, Days day, string time, bool isOverride, out string message)
        {
            if (!p.IsAvailableRange(day, time) && !isOverride)
            {
                message = $"{p.Name} is not available at this time or day. Enter override mode to override.";
                return false;
            }

            if (TheSchedule.ContainsKey(day))
            {
                if (TheSchedule[day].ContainsKey(p.Name))
                {
                    message = $"{p.Name} is already scheduled for {day}!";
                    return false;
                }
            }
            else
            {
                TheSchedule.Add(day, new Dictionary<string, string>());
            }

            TheSchedule[day].Add(p.Name, time);
            message = $"{p.Name} has been added to {day} with time {time}";
            return true;
        }

        public bool Remove(Person p, Days day, out string message)
        {
            if (TheSchedule.ContainsKey(day))
            {
                if (TheSchedule[day].ContainsKey(p.Name))
                {
                    TheSchedule[day].Remove(p.Name);
                    message = $"{p.Name} has been removed from {day}.";
                    return true;
                }
            }

            message = $"{p.Name} is not scheduled on {day}.";
            return false;
        }

        public void Write()
        {
            string path = Path.Combine(Paths.SchedulesFolder, DateTime.UtcNow.Year.ToString(), $"{WeekOf}-Schedule.json");
            string json = JsonConvert.SerializeObject(TheSchedule, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}
