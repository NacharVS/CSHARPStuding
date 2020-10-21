using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling.Poisoning
{
    public sealed class Poisoning
    {
        
        public delegate void ChangedDelegate(int poison, int value);

        private int _max;
        private int _value;

        public Poisoning(int max)
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

                if (IsIntoxicated)
                    HealthFall();
                
            }
        }
        public static void HealthFall()
        {
            Health health = new Health();
            for (int i = 0; i < 10; i++)
            {
                
                if (health.Value < 1)
                {
                   DeathEvent?.Invoke();
                }
                else
                health.Value = health.Value - health.Value * 5 / 100;

                Thread.Sleep(2000);
            }
        }
        public bool IsNotIntoxicated => _value < 0;
        public bool IsIntoxicated => _value >= _max;


        public event ChangedDelegate ValueChangedEvent;
        public event ChangedDelegate MaxChangedEvent;
        public static event Action DeathEvent;

        public static implicit operator int(Poisoning poison) => poison.Value;

        public static Poisoning operator +(Poisoning poison, int value)
        {
            poison.ValueAdd(value);
            return poison;
        }

        public static Poisoning operator -(Poisoning poison, int value)
        {
            poison.ValueRemove(value);
            return poison;
        }
    }
    
}
