﻿using HealthControlling;
using SatietyControlling;
using System;
using System.Data;

namespace Baffs
{
    class SatietyHealthBaff : Baff
    {
        private Health _health;
        private Satiety _satiety;
        private int _oldMax;
        private double _valueUpdateCoef;
        public SatietyHealthBaff(Satiety satiety, Health health) : base("Satiety", 1, 0)
        {
            _health = health;
            _satiety = satiety;
            _satiety.ValueChangedEvent += Update;
        }


        public void Update(int satiety, int value)
        {
            if (_satiety.Value / _satiety.Max < 60)
            {
                BaffStrength = 1;
                Activate();
            }
            else if (_satiety.Value / _satiety.Max < 30)
            {
                BaffStrength = 2;
                Activate();
            }
            else if (_satiety.Value / _satiety.Max == 0)
            {
                BaffStrength = 3;
                Activate();
            }
            else Deactivate();
            
            
        }
        public override void Activate()
        {
            switch (BaffStrength)
            {
                case 0:
                    Deactivate();
                    break;
                case 1:
                    _oldMax = Convert.ToInt32(_health.Max*1 / _valueUpdateCoef);
                    _health.MaxSet(Convert.ToInt32(_oldMax * 0.9));
                    _valueUpdateCoef=0.9;
                    _health.Value = (int)_valueUpdateCoef * _health.Value;
                    break;
                case 2:
                    _oldMax = Convert.ToInt32(_health.Max*1/_valueUpdateCoef);
                    _health.MaxSet(Convert.ToInt32(_oldMax * 0.6));
                    _valueUpdateCoef=0.6;
                    _health.Value = (int)_valueUpdateCoef * _health.Value;
                    break;
                case 3:
                    _oldMax = Convert.ToInt32(_health.Max*1/_valueUpdateCoef);
                    _health.MaxSet(Convert.ToInt32(_oldMax * 0.5));
                    _valueUpdateCoef=0.5;
                    _health.Value = (int)_valueUpdateCoef * _health.Value;
                    break;
                default:
                    break;
            }
            base.Activate();
        }
        public override void Deactivate()
        {
            _health.MaxSet(_oldMax);
            _health.Value = (int)(1 / _valueUpdateCoef) * _health.Value;
            base.Deactivate();
        }
    }
}
