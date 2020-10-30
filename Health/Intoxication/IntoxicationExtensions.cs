using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling.Intoxication
{
    public static class IntoxicationExtensions
    {
        public static (int, int) ValueSet(this Intoxication intoxication, int value)
        {
            var current = intoxication.Value;
            intoxication.Value = value;
            var diff = current - intoxication.Value;
            return (intoxication.Value, diff);
        }

        public static (int, int) ValueAdd(this Intoxication intoxication, int value)
        {
            var current = intoxication.Value;
            intoxication.Value += value;
            var diff = intoxication.Value - current;
            return (intoxication.Value, diff);
        }

        public static (int, int) ValueRemove(this Intoxication intoxication, int value)
        {
            var current = intoxication.Value;
            intoxication.Value -= value;
            var diff = current - intoxication.Value;
            return (intoxication.Value, diff);
        }

        public static (int, int) MaxSet(this Intoxication intoxication, int value)
        {
            var current = intoxication.Max;
            intoxication.Max = value;
            var diff = intoxication.Max - current;
            return (intoxication.Max, diff);
        }

        public static (int, int) MaxAdd(this Intoxication intoxication, int value)
        {
            var current = intoxication.Max;
            intoxication.Max += value;
            var diff = intoxication.Max - current;
            return (intoxication.Max, diff);
        }

        public static (int, int) MaxRemove(this Intoxication intoxication, int value)
        {
            var current = intoxication.Max;
            intoxication.Max -= value;
            var diff = intoxication.Max - current;
            return (intoxication.Max, diff);
        }
    }
}
