using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkScheduler.Structures
{
    public class Availability
    {
        public Availability(Days days, string range)
        {
            Days = days;
            Range = range;
        }

        public Days Days { get; }
        public string Range { get; }

        public bool IsAvailable(Days day, string time)
        {
            if (!Days.HasFlag(day)) return false;
            return Helpers.InRange(Range, time);
        }
    }
}
