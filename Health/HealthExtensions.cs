namespace HealthControlling
{
    //расширение класса Health
    public static class HealthExtension
    {
        public static (int, int) ValueSet(this Health health, int value)
        {
            var current = health.Value;
            health.Value = value;
            var diff = current - health.Value;
            return (health.Value, diff);
        }

        public static (int, int) ValueAdd(this Health health, int value)
        {
            var current = health.Value;
            health.Value += value;
            var diff = health.Value - current;
            return (health.Value, diff);
        }

        public static (int, int) ValueRemove(this Health health, int value)
        {
            var current = health.Value;
            health.Value -= value;
            var diff = current - health.Value;
            return (health.Value, diff);
        }

        public static (int, int) MaxSet(this Health health, int value)
        {
            var current = health.Max;
            health.Max = value;
            var diff = health.Max - current;
            return (health.Max, diff);
        }

        public static (int, int) MaxAdd(this Health health, int value)
        {
            var current = health.Max;
            health.Max += value;
            var diff = health.Max - current;
            return (health.Max, diff);
        }

        public static (int, int) MaxRemove(this Health health, int value)
        {
            var current = health.Max;
            health.Max -= value;
            var diff = health.Max - current;
            return (health.Max, diff);
        }
    }
}