using System;

namespace WorkScheduler
{
    [Flags]
    public enum Days
    {
        Unknown = 0,
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64,
    }
}
