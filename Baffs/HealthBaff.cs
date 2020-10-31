using HealthControlling;
using System;

namespace Baffs
{
    class HealthBaff: Baff
    {
        //Возможны доработки
        private Health _health;
        private int _oldMax;
        private double _valueUpdateCoef;

        public TimeSpan EffectTime { get; set; }

        public HealthBaff(Health health,string name, int strength, int time) : base(name,strength, TimeSpan.FromSeconds(time))
        {
            _health = health;
            EffectTime = TimeSpan.FromSeconds(time);
        }

        //В данном случае бафф временно увеличивает/уменьшает максимальное количество здоровья в зависимости от показателя силы баффа,
        //а так же пропорциионально изменению максимального здоровья увеличинивает/уменьшает его значение 
        public override void Activate(DateTime current)
        {
            _oldMax = _health.Max;
            _health.MaxAdd(BaffStrength);
            _valueUpdateCoef = _health.Max / _oldMax;
            _health.Value=(int)_valueUpdateCoef*_health.Value;

            base.Activate(current);
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
