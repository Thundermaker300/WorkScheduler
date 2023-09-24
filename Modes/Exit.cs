using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkScheduler.Modes
{
    public class Exit : IMode
    {
        public string Name => "exit";

        public string Desc => "Goodbye!";

        public void Run()
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
        }
    }
}
