using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkScheduler.Structures;
using WorkScheduler.Modes;
using Newtonsoft.Json;

namespace WorkScheduler
{
    public class Program
    {
        public static List<IMode> Modes { get; } = new()
        {
            new DebugRange(),
            new Modes.Schedule(),
            new EmployeeManager(),
            new Exit(),
        };

        public static List<Person> People { get; set; } = new()
        {
        };

        public static void Main(string[] args)
        {
            if (!Directory.Exists(Paths.MainDirectory))
                Directory.CreateDirectory(Paths.MainDirectory);

            if (!Directory.Exists(Paths.SchedulesFolder))
                Directory.CreateDirectory(Paths.SchedulesFolder);

            if (!Directory.Exists(Path.Combine(Paths.SchedulesFolder, DateTime.UtcNow.Year.ToString())))
                Directory.CreateDirectory(Path.Combine(Paths.SchedulesFolder, DateTime.UtcNow.Year.ToString()));

            if (!File.Exists(Paths.PeopleJson))
            {
                using (var sr = File.CreateText(Paths.PeopleJson))
                {
                    sr.WriteLine("[]");
                    sr.Close();
                }
            }

            try
            {
                People = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(Paths.PeopleJson));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load employee list. Please fix or delete people file. Press any key to close. Error: {ex.Message}");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Please enter mode. Valid modes:");
            foreach (IMode modeer in Modes)
            {
                Console.WriteLine($"- {modeer.Name} ({modeer.Desc})");
            }

            string mode = Console.ReadLine().ToLower();

            IMode imode = Modes.FirstOrDefault(m => m.Name == mode);
            imode.Run();
            Main(args);
        }
    }
}
