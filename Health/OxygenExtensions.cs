namespace HealthControlling
{
    public static class OxygenExtension
    {
        public static (int, int) ValueSet(this Oxygen oxygen, int value)
        {
            var current = oxygen.Value;
            oxygen.Value = value;
            var diff = current - oxygen.Value;
            return (oxygen.Value, diff);
        }

        public static (int, int) ValueAdd(this Oxygen oxygen, int value)
        {
            var current = oxygen.Value;
            oxygen.Value += value;
            var diff = oxygen.Value - current;
            return (oxygen.Value, diff);
        }

        public static (int, int) ValueRemove(this Oxygen oxygen, int value)
        {
            var current = oxygen.Value;
            oxygen.Value -= value;
            var diff = current - oxygen.Value;
            return (oxygen.Value, diff);
        }

        public static (int, int) MaxSet(this Oxygen oxygen, int value)
        {
            var current = oxygen.Max;
            oxygen.Max = value;
            var diff = oxygen.Max - current;
            return (oxygen.Max, diff);
        }

        public static (int, int) MaxAdd(this Oxygen oxygen, int value)
        {
            var current = oxygen.Max;
            oxygen.Max += value;
            var diff = oxygen.Max - current;
            return (oxygen.Max, diff);
        }

        public static (int, int) MaxRemove(this Oxygen oxygen, int value)
        {
            var current = oxygen.Max;
            oxygen.Max -= value;
            var diff = oxygen.Max - current;
            return (oxygen.Max, diff);
        }
    }
}
