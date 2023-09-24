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
            if (!Helpers.IsRange(range, out float firstNumber, out float secondNumber))
                return false;

            foreach (Availability available in Available)
            {
                if (!available.Days.HasFlag(day))
                    continue;
                if (!Helpers.IsRange(available.Range, out float start, out float end))
                    continue;

                if (firstNumber >= start && secondNumber < end)
                    return true;
            }

            return false;
        }
    }
}
