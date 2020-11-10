namespace HealthControlling.IsRadiation
{
    public interface IRadiationSource
    {
        int Value { get; }
        bool End { get; }

        int GetValue();
    }
}
