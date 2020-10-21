using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling.Poisoning
{
    public static class PoisoningExtensions
    {
        public static (int, int) ValueSet(this Poisoning poison, int value)
        {
            var current = poison.Value;
            poison.Value = value;
            var diff = current - poison.Value;
            return (poison.Value, diff);
        }

        public static (int, int) ValueAdd(this Poisoning poison, int value)
        {
            var current = poison.Value;
            poison.Value += value;
            var diff = poison.Value - current;
            return (poison.Value, diff);
        }

        public static (int, int) ValueRemove(this Poisoning poison, int value)
        {
            var current = poison.Value;
            poison.Value -= value;
            var diff = current - poison.Value;
            return (poison.Value, diff);
        }

        public static (int, int) MaxSet(this Poisoning poison, int value)
        {
            var current = poison.Max;
            poison.Max = value;
            var diff = poison.Max - current;
            return (poison.Max, diff);
        }

        public static (int, int) MaxAdd(this Poisoning poison, int value)
        {
            var current = poison.Max;
            poison.Max += value;
            var diff = poison.Max - current;
            return (poison.Max, diff);
        }

        public static (int, int) MaxRemove(this Poisoning poison, int value)
        {
            var current = poison.Max;
            poison.Max -= value;
            var diff = poison.Max - current;
            return (poison.Max, diff);
        }
    }
}
