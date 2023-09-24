using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkScheduler.Modes
{
    public class DebugRange : IMode
    {
        public string Name => "debug-range";
        public string Desc => "Debug the range logic.";

        public void Run()
        {
            Console.Write("Enter first (or 'exit' to stop): ");
            string first = Console.ReadLine();

            if (first.ToLower() == "exit")
                return;

            Console.WriteLine();

            Console.Write("Enter second: ");
            string second = Console.ReadLine();

            Console.WriteLine();

            Console.Write("Enter time: ");
            string time = Console.ReadLine();

            Console.WriteLine(Helpers.InRange($"{first}-{second}", time));
            Console.WriteLine("-----");
            Run();
        }
    }
}
