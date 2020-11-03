using HealthControlling;
using SatietyControlling;
using System;
using Baffs;

namespace CharacteristicControllingSystem
{
    public class SatietyHealthControl
    {
        private Health _health;
        private Satiety _satiety;
        private int _oldMax;
        private double _valueUpdateCoef;
        private Hunger _hunger;

        public SatietyHealthControl(Satiety satiety, Health health)
        {
            _health = health;
            _satiety = satiety;

            //В случае изменения значения сытости изменится и данный объект.
            _satiety.ValueChangedEvent += UpdateSatiety;
            _oldMax = _health.Max;
        }

        public void Update()
        {
            if (!(_hunger is null)) _hunger.Update(DateTime.Now);
        }
        
        private void UpdateSatiety(int satiety, int value)
        {
            if (0.60 >= _satiety.ValuePercent && _satiety.ValuePercent > 0.30)
            {
                Activate(1);
            }
            else if (0.30 >= _satiety.ValuePercent && _satiety.ValuePercent > 0)
            {
                Activate(2);
            }
            else if (_satiety.ValuePercent == 0)
            {
                Activate(3);
            }
            else
            {
                Deactivate();
            }
        }

        public void Activate(int _satietyStrength)
        {
            switch (_satietyStrength)
            {
                case 1:
                    //возвращение старого максимального значения здоровья при изменение силы сытости
                    if (_valueUpdateCoef != 0)
                        _oldMax = Convert.ToInt32(_health.Max / _valueUpdateCoef);
                    else _oldMax = _health.Max;

                    _health.MaxSet(Convert.ToInt32(_oldMax * 0.9));
                    _valueUpdateCoef=0.9;
                    _health.Value = (int)_valueUpdateCoef * _health.Value;

                    //удаление эффекта голода
                    _hunger = null;
                    break;
                case 2:
                    //возвращение старого максимального значения здоровья при изменение сытости
                    if (_valueUpdateCoef != 0) 
                        _oldMax = Convert.ToInt32(_health.Max/_valueUpdateCoef);
                    else _oldMax = _health.Max;

                    _health.MaxSet(Convert.ToInt32(_oldMax * 0.6));
                    _valueUpdateCoef=0.6;
                    _health.Value = (int)_valueUpdateCoef * _health.Value;

                    //удаление эффекта голода
                    _hunger = null;
                    break;
                case 3:
                    //возвращение старого максимального значения здоровья при изменение силы сытости
                    if (_valueUpdateCoef != 0) 
                        _oldMax = Convert.ToInt32(_health.Max/_valueUpdateCoef);
                    else _oldMax = _health.Max;

                    _health.MaxSet(Convert.ToInt32(_oldMax * 0.5));
                    _valueUpdateCoef=0.5;
                    _health.Value = (int)_valueUpdateCoef * _health.Value;
                    
                    //наложение эффекта голода
                    _hunger = new Hunger(_health);
                    break;
                default:
                    break;
            }
        }
        public void Deactivate()
        {
            //Отменяет последствия пониженой сытости
            if (_valueUpdateCoef != 0)
            {
                _health.MaxSet(_oldMax);
                _health.Value = (int)(_health.Value / _valueUpdateCoef);
            }
            _valueUpdateCoef = 0;_oldMax = _health.Max;
        }
    }
}
