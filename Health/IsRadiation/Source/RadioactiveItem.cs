namespace HealthControlling.IsRadiation.Source
{
    public sealed class RadioactiveItem : IRadiationSource //Радиоактивная еда/вода, любой предмет который можно употрибить
    {
        public int Value { get; private set; }
        public bool End { get; private set; }

        public RadioactiveItem(int value)
        {
            Value = value;
        }

        public int GetValue()
        {
            End = true;
            return Value;
        }
    }
}
