using HealthControlling;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationControlling
{
    class RadiationBaff
    {
        public delegate void DebuffStatusChange();

        private Health _health;
        private Radiation _radiation;
        private double _RateOfChange = 1;

        public RadiationBaff(Health health, Radiation radiation)
        {
            _health = health;
            _radiation = radiation;
            _radiation.ValueChangedEvent += LevelChange;
        }

        public void LevelChange(int radiation, int diff)
        {
            if (_radiation.Value / _radiation.Max < 25)
            {
                RadiationDebuff(0);
            }
            else if (_radiation.Value / _radiation.Max < 50)
            {
                RadiationDebuff(1);
            }
            else if (_radiation.Value / _radiation.Max < 75)
            {
                RadiationDebuff(2);
            }
            else if (_radiation.Value / _radiation.Max < 95)
            {
                RadiationDebuff(3);
            }
            else
            {
                RadiationDebuff(4);
            }
        }

        public void RadiationDebuff(int level)
        {
            _health.MaxSet(Convert.ToInt32(_health.Max / _RateOfChange)); // Возращает max и value здоровья к изночальным значением, чтобы не стакать дебафы
            _health.ValueSet(Convert.ToInt32(_health.Value / _RateOfChange));
            switch (level)
            {
                case 0:
                    DebuffDeacivateEvent?.Invoke();
                    break;
                case 1:
                    _RateOfChange = 0.8;
                    _health.MaxSet(Convert.ToInt32(_health.Max * _RateOfChange));
                    _health.ValueSet(Convert.ToInt32(_health.Value * _RateOfChange));
                    break;
                case 2:
                    _RateOfChange = 0.65;
                    _health.MaxSet(Convert.ToInt32(_health.Max * _RateOfChange));
                    _health.ValueSet(Convert.ToInt32(_health.Value * _RateOfChange));
                    break;
                case 3:
                    _RateOfChange = 0.5;
                    _health.MaxSet(Convert.ToInt32(_health.Max * _RateOfChange));
                    _health.ValueSet(Convert.ToInt32(_health.Value * _RateOfChange));
                    break;
                case 4:
                    _RateOfChange = 0.1;
                    _health.MaxSet(Convert.ToInt32(_health.Max * _RateOfChange));
                    _health.ValueSet(Convert.ToInt32(_health.Value * _RateOfChange));
                    break;
            }
                
        }

        public event DebuffStatusChange DebuffActivateEvent;
        public event DebuffStatusChange DebuffDeacivateEvent;
    }
}
