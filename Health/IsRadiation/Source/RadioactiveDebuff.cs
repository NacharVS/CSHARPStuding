namespace HealthControlling.IsRadiation.Source
{
    public sealed class RadioactiveDebuff : IRadiationSource //Вас ударили радиоактивной палкой? Ловите дебаф на n секунд
    {
        public int Value { get; private set; }
        public bool End { get; private set; }

        private int _time;

        public RadioactiveDebuff(int value, int time)
        {
            Value = value;
            _time = time;
        }

        public int GetValue()
        {
            _time--;
            if (_time == 0) End = true;
            return Value;
        }
    }
}
