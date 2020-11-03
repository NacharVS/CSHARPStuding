namespace HealthControlling.IsRadiation
{
    public sealed class Radiation
    {
        public delegate void ChangedDelegate(int health, int value);

        private int _max;
        private int _value;
        public Radiation(int max)
        {
            _max = max;
        }

        public int Max
        {
            get => _max;
            set
            {
                if (value == _max)
                    return;

                var oldMax = _max;
                _max = value;
                var maxDiff = _max - oldMax;

                if (_value > _max)
                {
                    var oldValue = _value;
                    _value = _max;
                    var diff = _value - oldValue;
                    ValueChangedEvent?.Invoke(_value, diff);
                }

                MaxChangedEvent?.Invoke(_max, maxDiff);
            }
        }

        public int Value
        {
            get => _value;
            set
            {
                if (value == _value)
                    return;

                var oldValue = _value;
                _value = value > Max ? Max : value;
                if (_value < 0)
                    _value = 0;
                var diff = _value - oldValue;

                ValueChangedEvent?.Invoke(_value, diff);
            }
        }

        public event ChangedDelegate ValueChangedEvent;
        public event ChangedDelegate MaxChangedEvent;

        public static implicit operator int(Radiation radiation) => radiation.Value;

        public static Radiation operator +(Radiation radiation, int value)
        {
            radiation.ValueAdd(value);
            return radiation;
        }

        public static Radiation operator -(Radiation radiation, int value)
        {
            radiation.ValueRemove(value);
            return radiation;
        }
    }
}
