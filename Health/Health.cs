using System;

namespace HealthControlling
{
    public sealed class Health
    {
        public delegate void ChangedDelegate(int health, int value);

        private int _max;
        private int _value;

        public Health(int max)
            : this(max, max)
        {
        }

        public Health(int max, int value)
        {
            _max = max;
            _value = value <= max
                ? value
                : max;
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

                if (IsDead)
                    DeathEvent?.Invoke();
            }
        }

        public bool IsAlive => _value > 0;
        public bool IsDead => _value <= 0;


        public event ChangedDelegate ValueChangedEvent;
        public event ChangedDelegate MaxChangedEvent;
        public event Action DeathEvent;

        public static implicit operator int(Health health) => health.Value;

        public static Health operator +(Health health, int value)
        {
            health.ValueAdd(value);
            return health;
        }

        public static Health operator -(Health health, int value)
        {
            health.ValueRemove(value);
            return health;
        }
    }
}
