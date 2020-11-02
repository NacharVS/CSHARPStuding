using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling.Intoxication
{
    public sealed class Intoxication
    {

        public delegate void ChangedDelegate(int intoxication, int value);

        private int _max;
        private int _value;

        public Intoxication(int max, int value)
        {
            _max = max;
            _value = value;
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

        public bool IsNotIntoxicated => _value <= 0;
        public bool IsIntoxicated => _value > 0;



        public event ChangedDelegate ValueChangedEvent;
        public event ChangedDelegate MaxChangedEvent;


        public static implicit operator int(Intoxication intoxication) => intoxication.Value;

        public static Intoxication operator +(Intoxication intoxication, int value)
        {
            intoxication.ValueAdd(value);
            return intoxication;
        }

        public static Intoxication operator -(Intoxication intoxication, int value)
        {
            intoxication.ValueRemove(value);
            return intoxication;
        }
    }
    
}
