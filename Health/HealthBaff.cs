using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HealthControlling
{
    class HealthBaff: Baff
    {
        //Возможны доработки
        private Health _health;
        private int _oldMax;
        private double _valueUpdateCoef;

        public HealthBaff(Health health,string name, int strength, double time) : base(name,strength,time)
        {
            _health = health;
        }

        //В данном случае бафф временно увеличивает/уменьшает максимальное количество здоровья в зависимости от показателя силы баффа,
        //а так же пропорциионально изменению максимального здоровья увеличинивает/уменьшает его значение 
        public override void Activate()
        {
            _oldMax = _health.Max;
            _health.MaxAdd(BaffStrength);
            _valueUpdateCoef = _health.Max / _oldMax;
            _health.Value=(int)_valueUpdateCoef*_health.Value;

            base.Activate();
        }

        public override void Deactivate()
        {
            //Возвращается старое маскимальное значение хп и так же вновь пропорционально уменьшенное/увеличенное значение value
            _health.Max = _oldMax;
            _health.Value = (int)(1/_valueUpdateCoef) * _health.Value;


            base.Deactivate();
        }
    }
}
