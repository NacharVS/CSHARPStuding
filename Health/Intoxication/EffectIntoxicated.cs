using System;

namespace HealthControlling.Intoxication
{
    public enum EIntoxicationCondition
    {
        Normal,
        Low
    }

    public sealed class EffectIntoxicated
    {
        private readonly Intoxication _intoxication;
        private readonly Health _health;
        private DateTime _lastLowTime;

        public EffectIntoxicated(Intoxication intoxication, Health health)
        {
            _intoxication = intoxication;
            _health = health;
        }

        public TimeSpan EffectTime { get; set; } = TimeSpan.FromSeconds(2);

        public void Update(DateTime current, EIntoxicationCondition condition)
        {
            if (_health <= 0)
                return;

            UpdateIntoxication(current, condition);
            UpdateHealth();
        }

        private void UpdateIntoxication(DateTime current, EIntoxicationCondition condition)
        {
            var persents = 0.05;
            if (condition == EIntoxicationCondition.Low)
            {
                persents = 0.1;
                _lastLowTime = current;
            }
            else
            {
                var delta = current - _lastLowTime;
                if (delta <= EffectTime)
                    persents = 0.05;
            }

            var value = (int)(_intoxication.Max * persents);
            _intoxication.ValueAdd(value);
        }

        private void UpdateHealth()
        {
            var percents = 0.0;

            if (_intoxication <= 0)
                percents = -0.05;
            else
            {
                var limit = _intoxication.Max * 0.5;
                if (_intoxication < limit)
                    percents = -0.01;
                else if (limit < _intoxication)
                    percents = -0.05;
                else
                    return;
            }

            var value = (int)(_health.Value * percents);
            _health.ValueAdd(value);
        }
    }
}
