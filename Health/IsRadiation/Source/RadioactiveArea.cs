namespace HealthControlling.IsRadiation.Source
{
    public sealed class RadioactiveArea : IRadiationSource //Радиоктивная зона
    {
        public int Value { get; private set; }
        public bool End { get; set; }

        public RadioactiveArea(int value)
        {
            Value = value;
        }

        public int GetValue()
        {
            return Value;
        }
    }
}
