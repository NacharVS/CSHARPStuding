using System;

namespace HealthControlling
{
    public enum EOxygenCondition
    {
        Normal,
        Low
    }

    public sealed class EffectHealthOxygen
    {
        private readonly Oxygen _oxygen;
        private readonly Health _health;
        private DateTime _lastLowTime;

        public EffectHealthOxygen(Oxygen oxygen, Health health)
        {
            _oxygen = oxygen;
            _health = health;
        }

        public TimeSpan EffectTime { get; set; } = TimeSpan.FromSeconds(2);

        //Задачи:
        //доделать апдейт кислорода
        //сделать метод окончания метода

        public void Update(DateTime current, EOxygenCondition condition)
        {
            if (_health <= 0)
                return;

            UpdateOxygen(current, condition);
            UpdateHealth();
        }

        private void UpdateOxygen(DateTime current, EOxygenCondition condition)
        {
            var persents = 0.075;
            if (condition == EOxygenCondition.Low)
            {
                persents = -0.1;
                _lastLowTime = current;
            }
            else
            {
                var delta = current - _lastLowTime;
                if (delta <= EffectTime)
                    persents = -0.05;
            }

            var value = (int) (_oxygen.Max * persents);
            _oxygen.ValueAdd(value);
        }

        private void UpdateHealth()
        {
            var percents = 0.0;

            if (_oxygen <= 0)
                percents = -0.05;
            else
            {
                var limit = _oxygen.Max * 0.2;
                if (_oxygen < limit)
                    percents = -0.01;
                else if (limit < _oxygen.Max)
                    percents = 0.05;
                else
                    return;
            }

            var value = (int)(_health.Value * percents);
            _health.ValueAdd(value);
        }
    }
}
