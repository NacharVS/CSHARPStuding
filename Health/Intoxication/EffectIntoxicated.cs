using System;

namespace HealthControlling.Intoxication
{


    public sealed class EffectIntoxicated
    {
        private readonly Intoxication _intoxication;
        private readonly Health _health;
        private readonly int _oldMax;
        private readonly Constant _constant = new Constant();
        public bool End { get; private set; }

        public EffectIntoxicated(Intoxication intoxication, Health health)
        {
            _intoxication = intoxication;
            _health = health;
            _oldMax = _health.Max;
        }

        public TimeSpan EffectTime { get; set; } = TimeSpan.FromSeconds(2);

        public void Update()
        {

            if (_health.Value <= 0)
                return;

            Console.WriteLine($"health Value:{_health.Value} Max:{_health.Max} ");
            Console.WriteLine($"intoxication Value:{_intoxication.Value} Max:{_intoxication.Max} ");
            Console.WriteLine();

            UpdateIntoxication();
            UpdateHealth();


            if (_intoxication.Value == 0)
            {
                End = true;
            }
        }
        
        private void UpdateIntoxication()
        {
            if (_intoxication.IsIntoxicated)
            {
                var value = (int)(_intoxication.Max * _constant.PercentChangeIntocsication);
                _intoxication.ValueAdd(value);
            }
        }

        private void UpdateHealth()
        {
            int value= 0;
            int valueM = 0;
            if (_intoxication > 0)
            {
                var limit = (int)(_intoxication.Max * _constant.PercentForLimitHealth);

                if (_intoxication < limit)
                {
                    value = (int)(_health.Value * _constant.MinPercentChangeHealth);
                }
                else if (_intoxication >= limit)
                {
                    value = (int)(_health.Value * _constant.MaxPercentChangeHealth);
                    valueM = (int)(_oldMax * _constant.PercentChangeMaxHealth);
                }

                if (value <= -1)
                {
                    value = _constant.MinPossibleChangeValueHealth;
                }
                _health.ValueAdd(value);
                _health.MaxAdd(valueM);
            }
        }
    }
    
}
