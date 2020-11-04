using System;

namespace HealthControlling
{
    public enum EOxygenCondition
    {
        Normal,
        Low
    }

    public sealed class EffectOxygenHunger
    {
        private readonly Oxygen _oxygen;
        private readonly Health _health;
        private DateTime _lastLowTime;

        public EffectOxygenHunger(Oxygen oxygen, Health health)
        {
            _oxygen = oxygen;
            _health = health;
        }

        public TimeSpan EffectTime { get; set; } = TimeSpan.FromSeconds(5);

        public void Update(DateTime current, EOxygenCondition condition)
        {
            if (_health <= 0)
                return;

            UpdateOxygen(current, condition);
            UpdateHealth();
        }

        private void UpdateOxygen(DateTime current, EOxygenCondition condition)
        {
            var percents = 0.075;
            if (condition == EOxygenCondition.Low)
            {
                percents = -0.05;
                _lastLowTime = current;
            }
            else
            {
                var delta = current - _lastLowTime;
                if (delta <= EffectTime)
                    percents = -0.01;
            }

            Console.Write($"Percents oxygen: {percents} ");
            var value = (int) (_oxygen.Max * percents);
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
                    percents = -0.005;
                else if (limit <= _oxygen.Max)
                    percents = 0;
                else if (limit == _oxygen.Max)
                    percents = 0.01;
                else
                    return;
            }

            Console.Write($"Percents health: {percents}\n");
            var value = (int)(_health.Max * percents);
            _health.ValueAdd(value);
        }
    }
}
