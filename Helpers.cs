using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WorkScheduler.Structures;

namespace WorkScheduler
{
    public static class Helpers
    {
        public static Dictionary<Days, int> ToAdd { get; } = new Dictionary<Days, int>()
        {
            [Days.Sun] = 0,
            [Days.Mon] = 1,
            [Days.Tue] = 2,
            [Days.Wed] = 3,
            [Days.Thu] = 4,
            [Days.Fri] = 5,
            [Days.Sat] = 6,
        };
        public static Person GetPerson(string name)
        {
            Person p = null;
            foreach (Person ps in Program.People)
            {
                if (ps.Name.ToLower().Contains(name))
                    return ps;
            }
            return p;
        }


        public static float ToNumber(string time)
        {
            try
            {
                DateTime dt = DateTime.Parse(time);
                float f = float.Parse(dt.ToString("HHmm"));

                if (f.ToString().Length > 2)
                    f /= 100;

                return f;
            }
            catch
            {
                return -1f;
            }
        }

        public static bool IsRange(string range, out float first, out float second)
        {
            first = -1f;
            second = -1f;

            string[] parts = range.Split('-');

            if (parts.Length != 2)
                return false;

            string firstStr = parts[0].ToLower();
            string secondStr = parts[1].ToLower();

            first = ToNumber(firstStr);
            second = ToNumber(secondStr);

            return first != -1f && second != -1f;
        }

        public static string GetEnding(string time)
        {
            return Regex.Replace(time, @"[\d-]", string.Empty).Trim().ToLower();
        }

        public static bool InRange(string range, string time)
        {
            if (!IsRange(range, out float firstNumber, out float secondNumber))
                return false;

            float timeNumber = ToNumber(time);

            if (firstNumber.ToString().Length > 2)
                firstNumber /= 100;

            if (secondNumber.ToString().Length > 2)
                secondNumber /= 100;

            if (timeNumber.ToString().Length > 2)
                timeNumber /= 100;

            return timeNumber >= firstNumber && timeNumber < secondNumber;
        }
    }
}
