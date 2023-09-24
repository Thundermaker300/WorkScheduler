using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkScheduler.Structures
{
    public class Person
    {
        public Person(string name, List<Availability> available)
        {
            Name = name;
            Available = available;
        }

        public string Name { get; }
        public List<Availability> Available { get; set; }

        public bool IsAvailable(Days day, out Availability availability)
        {
            availability = Available.FirstOrDefault(av => av.Days.HasFlag(day));
            return availability != null;
        }

        public bool IsAvailable(Days day)
            => IsAvailable(day, out _);

        public bool IsAvailableRange(Days day, string range)
        {
            string[] parts = range.Split('-');

            if (parts.Length != 2)
                return false;

            float firstNumber = Helpers.ToNumber(parts[0].ToLower());
            float secondNumber = Helpers.ToNumber(parts[1].ToLower());

            Console.WriteLine($"AV: {firstNumber}-{secondNumber}");

            foreach (Availability available in Available)
            {
                if (!available.Days.HasFlag(day))
                    continue;

                string[] parts2 = available.Range.Split('-');

                if (parts2.Length != 2)
                    return false;

                float start = Helpers.ToNumber(parts2[0].ToLower());
                float end = Helpers.ToNumber(parts2[1].ToLower());

                Console.WriteLine($"REQ: {start}-{end}");

                if (firstNumber >= start && secondNumber < end)
                    return true;
            }

            return false;
        }
    }
}
