using System;
using System.Collections;

namespace HealthControlling.IsRadiation
{
    public enum ERadiationLevel
    {
        Normal,
        Low,
        Hight
    }
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
            ERadiationLevel level = GetLevel(Source);
            UpdateRadiation(level);
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

        private void UpdateRadiation(ERadiationLevel level)
        {
            var percents = -0.05;
            if (level == ERadiationLevel.Hight)
            {
                percents = 0.05;
            }
            else if (level == ERadiationLevel.Low)
            {
                percents = 0.02;
            }
            var value = (int)(_radiation.Max * percents);
            _radiation.ValueAdd(value);
        }

        private void UpdateHealth()
        {
            _health.MaxSet((int)(_health.Max / _RateOfChange)); // Возращает max и value здоровья к изночальным значением, чтобы не стакать дебафы
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
        
        private ERadiationLevel GetLevel(ArrayList Source)
        {
            ERadiationLevel level = ERadiationLevel.Normal;
            foreach (IRadiationSource item in Source)
            {
                if (item.Level == ERadiationLevel.Hight)
                {
                    return ERadiationLevel.Hight;
                }
                else if (item.Level == ERadiationLevel.Low)
                {
                    if (item.Level == level) return ERadiationLevel.Hight;
                    level = item.Level;
                }
            }
            return level;
        }

        public event DebuffStatusChange ChangeLevelEvent;
    }
}
