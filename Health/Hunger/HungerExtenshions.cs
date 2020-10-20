using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling
{
   public static class HungerExtenshions
    {
        public static (int, int) ValueSet(this Hunger hunger, int value)
        {
            var current = hunger.Value;
            hunger.Value = value;
            var diff = current - hunger.Value;
            return (hunger.Value, diff);
        }

        public static (int, int) ValueAdd(this Hunger hunger, int value)
        {
            var current = hunger.Value;
            hunger.Value += value;
            var diff = hunger.Value - current;
            return (hunger.Value, diff);
        }

        public static (int, int) ValueRemove(this Hunger hunger, int value)
        {
            var current = hunger.Value;
            hunger.Value -= value;
            var diff = current - hunger.Value;
            return (hunger.Value, diff);
        }

        public static (int, int) MaxSet(this Hunger hunger, int value)
        {
            var current = hunger.Max;
            hunger.Max = value;
            var diff = hunger.Max - current;
            return (hunger.Max, diff);
        }

        public static (int, int) MaxAdd(this Hunger hunger, int value)
        {
            var current = hunger.Max;
            hunger.Max += value;
            var diff = hunger.Max - current;
            return (hunger.Max, diff);
        }

        public static (int, int) MaxRemove(this Hunger hunger, int value)
        {
            var current = hunger.Max;
            hunger.Max -= value;
            var diff = hunger.Max - current;
            return (hunger.Max, diff);
        }
    }
}
