namespace SatietyControlling
{
    public static class SatietyExtensions
    {
        public static (int, int) ValueSet(this Satiety satiety, int value)
        {
            var current = satiety.Value;
            satiety.Value = value;
            var diff = current - satiety.Value;
            return (satiety.Value, diff);
        }

        public static (int, int) ValueAdd(this Satiety satiety, int value)
        {
            var current = satiety.Value;
            satiety.Value += value;
            var diff = satiety.Value - current;
            return (satiety.Value, diff);
        }

        public static (int, int) ValueRemove(this Satiety satiety, int value)
        {
            var current = satiety.Value;
            satiety.Value -= value;
            var diff = current - satiety.Value;
            return (satiety.Value, diff);
        }

        public static (int, int) MaxSet(this Satiety satiety, int value)
        {
            var current = satiety.Max;
            satiety.Max = value;
            var diff = satiety.Max - current;
            return (satiety.Max, diff);
        }

        public static (int, int) MaxAdd(this Satiety satiety, int value)
        {
            var current = satiety.Max;
            satiety.Max += value;
            var diff = satiety.Max - current;
            return (satiety.Max, diff);
        }

        public static (int, int) MaxRemove(this Satiety satiety, int value)
        {
            var current = satiety.Max;
            satiety.Max -= value;
            var diff = satiety.Max - current;
            return (satiety.Max, diff);
        }
    }
}
