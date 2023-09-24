using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkScheduler.Structures;
using MySchedule = WorkScheduler.Structures.Schedule;

namespace WorkScheduler.Modes
{
    public class Schedule : IMode
    {
        public string Name => "sch";

        public string Desc => "Write a schedule!";

        public void Run()
        {
            Console.Write("Enter week of (eg. 9-10): ");
            string weekOf = Console.ReadLine();

            MySchedule sc = new MySchedule(weekOf);
            Days daysNeedingCovered = Days.All;

            while (true)
            {
                Console.WriteLine($"The following days are missing employee coverage: {daysNeedingCovered}");
                Console.Write("Enter mode (add/remove/done/exit): ");
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
                Add_EnterDay:
                    Console.WriteLine("Enter day to add: ");
                    string dayString = Console.ReadLine();
                    if (!Enum.TryParse(dayString, true, out Days day))
                    {
                        Console.WriteLine("Invalid day!");
                        goto Add_EnterDay;
                    }
                    Console.WriteLine($"Available employees on {day}:");
                    foreach (Person p in Program.People)
                    {
                        if (p.IsAvailable(day, out Availability availabilityForDay))
                            Console.WriteLine($"- {p.Name} ({availabilityForDay.Range})");
                    }

                Add_EnterEmployee:
                    Console.WriteLine("Enter employee to add (or exit to exit): ");
                    string emp = Console.ReadLine().ToLower();
                    if (emp == "exit")
                    {
                        continue;
                    }
                    Person employee = Helpers.GetPerson(emp);
                    if (employee == null)
                    {
                        Console.WriteLine("Invalid person!");
                        goto Add_EnterEmployee;
                    }

                    if (!employee.IsAvailable(day))
                    {
                        Console.WriteLine($"{employee.Name} is not available on {day}.");
                        goto Add_EnterEmployee;
                    }

                Add_TimeRange:
                    Console.WriteLine("Enter time range: ");
                    string time = Console.ReadLine();

                    string[] parts = time.Split('-');

                    if (parts.Length != 2)
                    {
                        Console.WriteLine("Invalid time range inputted.");
                        goto Add_TimeRange;
                    }

                    float firstNumber = Helpers.ToNumber(parts[0].ToLower());
                    float secondNumber = Helpers.ToNumber(parts[1].ToLower());

                    if (firstNumber == -1)
                    {
                        Console.WriteLine("Invalid starting time.");
                        goto Add_TimeRange;
                    }

                    if (secondNumber == -1)
                    {
                        Console.WriteLine("Invalid starting time.");
                        goto Add_TimeRange;
                    }

                    bool success = sc.Add(employee, day, time, isOverride, out string message);
                    Console.WriteLine(message);

                    if (success)
                    {
                        daysNeedingCovered &= ~day;
                    }

                }
                else if (mode == "remove")
                {
                    Console.WriteLine("Not implemented yet.");
                }
                else if (mode == "done")
                {
                    if (!isOverride)
                    {
                        bool deny = false;
                        foreach (Days day in Enum.GetValues(typeof(Days)))
                        {
                            if (!sc.TheSchedule.ContainsKey(day))
                            {
                                deny = true;
                                Console.WriteLine($"Missing any coverage for {day}. Please add coverage or enter override mode to continue.");
                            }
                        }
                        if (deny)
                            continue;
                    }

                    sc.Write();
                    Console.WriteLine($"File created at: Schedules/{DateTime.UtcNow.Year}/{weekOf}-Schedule.json");
                    Console.WriteLine("OVERRIDE MODE EXITED");
                    break;
                }

                if (isOverride)
                {
                    Console.WriteLine($"OVERRIDE MODE EXITED");
                }
            }
        }
    }
}
