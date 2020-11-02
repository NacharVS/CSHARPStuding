using System;

namespace HealthControlling.Intoxication
{


    public sealed class EffectIntoxicated
    {
        private readonly Intoxication _intoxication;
        private readonly Health _health;
        public bool End { get; private set; }



        public EffectIntoxicated(Intoxication intoxication, Health health)
        {
            _intoxication = intoxication;
            _health = health;
        }

        public TimeSpan EffectTime { get; set; } = TimeSpan.FromSeconds(2);

        public void Update()
        {
            if (_health <= 0)
                return;
            if (_intoxication.Value == 0)
            {
                End = true;
            }

            Console.WriteLine($"health Value:{_health.Value} Max:{_health.Max} ");
            Console.WriteLine($"intoxication Value:{_intoxication.Value} Max:{_intoxication.Max} ");
            UpdateIntoxication();
            UpdateHealth();
        }

        private void UpdateIntoxication()
        {
            var percents = 0.0;

            if (_intoxication.IsIntoxicated)
            {
                percents = -0.05;
            }


            var value = (int)(_intoxication.Max * percents);
            _intoxication.ValueAdd(value);
        }

        private void UpdateHealth()
        {
            var percents = 0.0;
            var percentsM = 0.0;

            if (_intoxication > 0)
            {
                var limit = _intoxication.Max * 0.5;
                if (_intoxication < limit)
                    percents = -0.03;
                else if (limit <= _intoxication)
                {
                    percents = -0.05;
                    percentsM = -0.02;
                }

                else
                    return;
            }

            var value = (int)(_health.Value * percents);
            var valueM = (int)(_health.Max * percentsM);
            _health.MaxAdd(valueM);
            _health.ValueAdd(value);
        }
    }
    
}
