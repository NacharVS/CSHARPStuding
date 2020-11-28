
namespace HealthControlling.Intoxication
{
    public class Constant
    {
        public double PercentChangeIntocsication { get; } = -0.05;
        public double PercentForLimitHealth { get; } = 0.5;
        public double MaxPercentChangeHealth { get; } = -0.04;
        public double MinPercentChangeHealth { get; } = -0.02;
        public double PercentChangeMaxHealth { get; } = -0.02;

        public int MinPossibleChangeValueHealth { get; }  = -2;
    }
}
