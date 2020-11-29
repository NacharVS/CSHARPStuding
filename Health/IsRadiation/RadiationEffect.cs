using System;
using System.Collections;

namespace HealthControlling.IsRadiation
{
    public sealed class RadiationEffect
    {
        public delegate void DebuffStatusChange(string level);

        private readonly Health _health;
        private readonly Radiation _radiation = new Radiation(100);
        private RadEffectConstant _constant;
        private double LastRateOfChange = 1;
        private int _count = 0;
        public bool End { get; private set; }

        public RadiationEffect(Health health, RadEffectConstant constant)
        {
            _health = health;
            _constant = constant;
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
            if (value == 0) value = -(_radiation.Max * _constant.PassiveRadiationExtraction/100);
            _radiation.ValueAdd(value);
        }

        private void UpdateHealth()
        {
            _health.MaxSet((int)(_health.Max / LastRateOfChange));
            _health.ValueSet((int)(_health.Value / LastRateOfChange));
            if (_radiation.Value*100 / _radiation.Max < _constant.LowLevel)
            {
                _count = 0;
                LastRateOfChange = 1;
                ChangeLevelEvent?.Invoke("Normal");
            }
            else if (_radiation.Value*100 / _radiation.Max < _constant.MediumLevel)
            {
                LastRateOfChange = _constant.RateOfChangeLow;
                _health.ValueSet((int)(_health.Value * _constant.RateOfChangeLow));
                _health.MaxSet((int)(_health.Max * _constant.RateOfChangeLow));
                ChangeLevelEvent?.Invoke("Low");
            }
            else if (_radiation.Value*100 / _radiation.Max < _constant.HightLevel)
            {
                LastRateOfChange = _constant.RateOfChangeMedium;
                _health.ValueSet((int)(_health.Value * _constant.RateOfChangeMedium));
                _health.MaxSet((int)(_health.Max * _constant.RateOfChangeMedium));
                ChangeLevelEvent?.Invoke("Medium");
            }
            else if (_radiation.Value*100 / _radiation.Max < _constant.DeadlyLevel)
            {
                LastRateOfChange = _constant.RateOfChangeHight;
                _health.ValueSet((int)(_health.Value * _constant.RateOfChangeHight));
                _health.MaxSet((int)(_health.Max * _constant.RateOfChangeHight));
                ChangeLevelEvent?.Invoke("Hight");
            }
            else
            {
                _count += _constant.DeadlyCountIncrease;
                LastRateOfChange = _constant.RateOfChangeDeadly;
                _health.ValueSet((int)(_health.Value * _constant.RateOfChangeDeadly));
                _health.MaxSet((int)(_health.Max * _constant.RateOfChangeDeadly));
                _health.ValueRemove((int)(_health.Max  * _count / 100));
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
