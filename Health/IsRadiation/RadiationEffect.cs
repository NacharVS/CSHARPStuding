using System;
using System.Collections;

namespace HealthControlling.IsRadiation
{
    public sealed class RadiationEffect
    {
        public delegate void DebuffStatusChange(string level);

        private readonly Health _health;
        private readonly Radiation _radiation = new Radiation(100);
        private double _RateOfChange = 1;
        private int _count = 0;
        public bool End { get; private set; }

        public RadiationEffect(Health health)
        {
            _health = health;
        }

        public void EffectUpdate(ArrayList Source)
        {
            int value = GetLevel(Source);
            UpdateRadiation(value);
            UpdateHealth();

            Console.WriteLine($"health Value:{_health.Value} Max:{_health.Max} "); // Времено для теста
            Console.WriteLine($"radiation Value:{_radiation.Value} Max:{_radiation.Max} "); // Времено для теста

            if (_health <= 0)
            {
                Console.WriteLine("Death"); // Времено для теста
                End = true;
                return;
            }

            if (_radiation <= 0)
            {
                Console.WriteLine("RadZERO"); // Времено для теста
                End = true;
                return;
            }
        }

        private void UpdateRadiation(int value)
        {
            if (value == 0) value = -(_radiation.Max * 5 / 100);
            _radiation.ValueAdd(value);
        }

        private void UpdateHealth()
        {
            _health.MaxSet((int)(_health.Max / _RateOfChange));
            _health.ValueSet((int)(_health.Value / _RateOfChange));
            _RateOfChange = 1;
            if (_radiation.Value*100 / _radiation.Max < 25)
            {
                _count = 0;
                ChangeLevelEvent?.Invoke("Normal");
            }
            else if (_radiation.Value*100 / _radiation.Max < 50)
            {
                _RateOfChange = 0.8;
                _health.ValueSet((int)(_health.Value * _RateOfChange));
                _health.MaxSet((int)(_health.Max * _RateOfChange));
                ChangeLevelEvent?.Invoke("Low");
            }
            else if (_radiation.Value*100 / _radiation.Max < 75)
            {
                _RateOfChange = 0.65;
                _health.ValueSet((int)(_health.Value * _RateOfChange));
                _health.MaxSet((int)(_health.Max * _RateOfChange));
                ChangeLevelEvent?.Invoke("Medium");
            }
            else if (_radiation.Value*100 / _radiation.Max < 95)
            {
                _RateOfChange = 0.5;
                _health.ValueSet((int)(_health.Value * _RateOfChange));
                _health.MaxSet((int)(_health.Max * _RateOfChange));
                ChangeLevelEvent?.Invoke("Hight");
            }
            else
            {
                _count += 1; 
                _RateOfChange = 0.1;
                _health.ValueSet((int)(_health.Value * _RateOfChange));
                _health.MaxSet((int)(_health.Max * _RateOfChange));
                if (_count > 3)
                {
                        _health.ValueAdd((int)(_health.Max * -0.1));
                }
                ChangeLevelEvent?.Invoke("Deadly");
            }
        }
        
        private int GetLevel(ArrayList Source)
        {
            ArrayList IndexOfEndsList = new ArrayList();
            int value = 0;
            foreach (IRadiationSource item in Source)
            {
                value += item.GetValue();
                if (item.End)
                    IndexOfEndsList.Add(Source.IndexOf(item));
            }

            IndexOfEndsList.Reverse();

            foreach (int item in IndexOfEndsList) //Удоляет все источники действия которых закончилось
            {
                Source.RemoveAt(item);
            }

            return value;
        }

        public event DebuffStatusChange ChangeLevelEvent;
    }
}
