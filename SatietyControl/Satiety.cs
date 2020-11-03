using System;
using System.Reflection.Metadata.Ecma335;

namespace SatietyControlling
{
    public sealed class Satiety
    {
        public delegate void ChangedDelegate(int satiety, int value);

        private int _max;
        private int _value;

        public Satiety(int max)
        {
            Max = max;
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
                if (_max < 0)
                {
                    _max = 0;
                }
                var maxDiff = _max - oldMax;

                if (_value > _max)
                {
                    Value = _max;
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


        public double ValuePercent => (double) _value / _max;

        public event ChangedDelegate ValueChangedEvent;
        public event ChangedDelegate MaxChangedEvent;

        public static implicit operator int(Satiety satiety) => satiety.Value;
        

        public static Satiety operator +(Satiety satiety, int value)
        {
            satiety.ValueAdd(value);
            return satiety;
        }

        public static Satiety operator -(Satiety satiety, int value)
        {
            satiety.ValueRemove(value);
            return satiety;
        }
    }
}
