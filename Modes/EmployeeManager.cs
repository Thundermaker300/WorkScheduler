using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkScheduler.Structures;

namespace WorkScheduler.Modes
{
    public class EmployeeManager : IMode
    {
        public string Name => "empmgr";

        public string Desc => "Add or remove employees from the system.";

        public void Run()
        {
            while (true)
            {
                Console.Write($"Enter mode (add/remove/save/exit): ");

                string[] content = Console.ReadLine().ToLower().Split(' ');

                if (content.Length < 0)
                    continue;

                string mode = content[0];
                bool isOverride = content.Length > 1 && content[1] == "override";

                if (mode == "exit")
                {
                    break;
                }

                if (isOverride)
                {
                    Console.WriteLine($"OVERRIDE MODE ENABLED");
                }

                if (mode == "add")
                {
                    Console.Write("Enter employee full name: ");

                    string name = Console.ReadLine();
                    List<Availability> available = new();

                    while (true)
                    {
                        Console.WriteLine("Enter an availability time-range (eg. 6am-2:30pm).");
                        Console.WriteLine("Enter 'done' to proceed.");
                        string range = Console.ReadLine();

                        if (range.ToLower() == "done")
                            break;

                        if (!Helpers.IsRange(range, out _, out _))
                        {
                            Console.WriteLine("Invalid time range provided.");
                            continue;
                        }

                        Days days = Days.Unknown;
                        while (true)
                        {
                            Console.WriteLine("Enter the days this employee can work (one at a time).");
                            Console.WriteLine("Enter 'done' to proceed.");

                            string dayStr = Console.ReadLine();
                            if (dayStr.ToLower() == "done")
                                break;

                            if (Enum.TryParse(dayStr, true, out Days day))
                                days |= day;
                        }

                        if (days == Days.Unknown)
                        {
                            Console.WriteLine("No availability provided for this time range. Time range skipped.");
                            continue;
                        }

                        Availability avail = new(days, range);
                        available.Add(avail);
                    }

                    Person p = new(name, available);
                    Program.People.Add(p);
                    Console.WriteLine($"New employee '{name}' successfully added. Make sure to save!");

                }
                else if (mode == "remove")
                {
                    Console.WriteLine("Not implemented yet.");
                }
                else if (mode == "save")
                {
                    string serialized = JsonConvert.SerializeObject(Program.People);
                    System.IO.File.WriteAllText(Paths.PeopleJson, serialized);
                    Console.WriteLine($"{Program.People.Count} employees have been saved.");
                }

                if (isOverride)
                {
                    Console.WriteLine($"OVERRIDE MODE EXITED");
                }
            }
        }
    }
}
