using HealthControlling;
using SatietyControlling;
using System;
using System.Data;
using System.Security.Cryptography;

namespace CharacteristicControllingSystem
{
    class SatietyHealthControl
    {
        private Health _health;
        private Satiety _satiety;
        private int _oldMax;
        private double _valueUpdateCoef;
        public SatietyHealthControl(Satiety satiety, Health health)
        {
            _health = health;
            _satiety = satiety;
            _satiety.ValueChangedEvent += UpdateSatiety;
        }


        public void UpdateSatiety(int satiety, int value)
        {
            if (_satiety.Value / _satiety.Max < 60)
            {
                Activate(1);
            }
            else if (_satiety.Value / _satiety.Max < 30)
            {
                Activate(2);
            }
            else if (_satiety.Value / _satiety.Max == 0)
            {
                Activate(3);
            }
            else Deactivate();
            
        }
        public void Activate(int _satietyStrength)
        {
            switch (_satietyStrength)
            {
                case 0:
                    Deactivate();
                    break;
                case 1:
                    _oldMax = Convert.ToInt32(_health.Max/ _valueUpdateCoef);
                    _health.MaxSet(Convert.ToInt32(_oldMax * 0.9));
                    _valueUpdateCoef=0.9;
                    _health.Value = (int)_valueUpdateCoef * _health.Value;
                    break;
                case 2:
                    _oldMax = Convert.ToInt32(_health.Max/_valueUpdateCoef);
                    _health.MaxSet(Convert.ToInt32(_oldMax * 0.6));
                    _valueUpdateCoef=0.6;
                    _health.Value = (int)_valueUpdateCoef * _health.Value;
                    break;
                case 3:
                    _oldMax = Convert.ToInt32(_health.Max/_valueUpdateCoef);
                    _health.MaxSet(Convert.ToInt32(_oldMax * 0.5));
                    _valueUpdateCoef=0.5;
                    _health.Value = (int)_valueUpdateCoef * _health.Value;
                    break;
                default:
                    break;
            }
            
        }
        public void Deactivate()
        {
            _health.MaxSet(_oldMax);
            _health.Value = (int)(_health.Value/_valueUpdateCoef);
        }
    }
}
