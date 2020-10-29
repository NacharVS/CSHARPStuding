using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationControlling
{
    public static class RadiationExtensions
    {
        public static (int, int) ValueSet(this Radiation radiation, int value)
        {
            var current = radiation.Value;
            radiation.Value = value;
            var diff = current - radiation.Value;
            return (radiation.Value, diff);
        }

        public static (int, int) ValueAdd(this Radiation radiation, int value)
        {
            var current = radiation.Value;
            radiation.Value += value;
            var diff = radiation.Value - current;
            return (radiation.Value, diff);
        }

        public static (int, int) ValueRemove(this Radiation radiation, int value)
        {
            var current = radiation.Value;
            radiation.Value -= value;
            var diff = current - radiation.Value;
            return (radiation.Value, diff);
        }

        public static (int, int) MaxSet(this Radiation radiation, int value)
        {
            var current = radiation.Max;
            radiation.Max = value;
            var diff = radiation.Max - current;
            return (radiation.Max, diff);
        }

        public static (int, int) MaxAdd(this Radiation radiation, int value)
        {
            var current = radiation.Max;
            radiation.Max += value;
            var diff = radiation.Max - current;
            return (radiation.Max, diff);
        }

        public static (int, int) MaxRemove(this Radiation radiation, int value)
        {
            var current = radiation.Max;
            radiation.Max -= value;
            var diff = radiation.Max - current;
            return (radiation.Max, diff);
        }
    }
}
