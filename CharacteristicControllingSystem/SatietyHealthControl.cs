using HealthControlling;
using SatietyControlling;
using System;
using Baffs;

namespace CharacteristicControllingSystem
{
    public class SatietyHealthControl
    {
        private readonly Health _health;
        private readonly Satiety _satiety;
        private int _oldMax;
        private double _valueUpdateCoef;
        private HungerBaff _hungerBaff;

        public SatietyHealthControl(Satiety satiety, Health health)
        {
            _health = health;
            _satiety = satiety;

            //В случае изменения значения сытости изменится и данный объект.
            _satiety.ValueChangedEvent += UpdateSatiety;
            _oldMax = _health.Max;
        }

        public void Update(DateTime dateTime)
        {
            if (_hungerBaff != null)
                _hungerBaff.Update(dateTime);
        }

        private void UpdateSatiety(int satiety, int value)
        {
            if (_satiety.ValuePercent > 60)
            {
                if (_valueUpdateCoef != 0)
                {
                    _health.MaxSet(_oldMax);
                    _health.Value = (int)(_health.Value / _valueUpdateCoef);
                }
                _valueUpdateCoef = 0;
                _oldMax = _health.Max;
                return;
            }

            _oldMax = _valueUpdateCoef != 0
                ? (int)(_health.Max / _valueUpdateCoef)
                : _health.Max;

            if (0.60 >= _satiety.ValuePercent && _satiety.ValuePercent > 0.30)
            {
                _valueUpdateCoef = 0.9f;
            }
            else if (0.30 >= _satiety.ValuePercent && _satiety.ValuePercent > 0)
            {
                _valueUpdateCoef = 0.6f;
            }
            else if (_satiety.ValuePercent == 0)
            {
                _valueUpdateCoef = 0.5f;
            }

            _health.MaxSet((int)(_oldMax * _valueUpdateCoef));
            _health.Value = (int)(_valueUpdateCoef * _health.Value);

            if (_satiety.Value > 0)
                _hungerBaff = null;
            else
            if (_hungerBaff == null)
                _hungerBaff = new HungerBaff(_health, _satiety);
        }
    }
}
