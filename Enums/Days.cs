using System;

namespace WorkScheduler
{
    [Flags]
    public enum Days
    {
        Unknown = 0,
        Sun = 1,
        Mon = 2,
        Tue = 4,
        Wed = 8,
        Thu = 16,
        Fri = 32,
        Sat = 64,
        All = 127,
    }
}
