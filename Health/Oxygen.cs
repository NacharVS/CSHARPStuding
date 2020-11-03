namespace HealthControlling
{
    public sealed class Oxygen
    {
        public delegate void ChangedDelegate(int Oxygen, int value);

        private int _max;
        private int _value;
        
        public Oxygen(int max)
        {
            _max = max;
            _value = max;
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

        public static implicit operator int(Oxygen oxygen) => oxygen.Value;

        public static Oxygen operator +(Oxygen oxygen, int value)
        {
            oxygen.ValueAdd(value);
            return oxygen;
        }

        public static Oxygen operator -(Oxygen oxygen, int value)
        {
            oxygen.ValueRemove(value);
            return oxygen;
        }
    }
}
