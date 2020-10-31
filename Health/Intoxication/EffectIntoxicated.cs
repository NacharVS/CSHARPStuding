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
        public bool End;


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
            Console.WriteLine($"health Value:{_health.Value} Max:{_health.Max} ");
            Console.WriteLine($"intoxication Value:{_intoxication.Value} Max:{_intoxication.Max} ");
            UpdateIntoxication();
            UpdateHealth();
        }

        private void UpdateIntoxication()
        {
            var percents = -0.03;

            if (_intoxication.condition == EIntoxicationCondition.Low)
            {
                percents = -0.05;
            }


            var value = (int)(_intoxication.Max * percents);
            _intoxication.ValueAdd(value);
        }

        private void UpdateHealth()
        {
            var percents = 0.0;

            if (_intoxication > 0)
            {
                var limit = _intoxication.Max * 0.5;
                if (_intoxication < limit)
                    percents = -0.02;
                else if (limit < _intoxication)
                    percents = -0.05;
                else
                    return;
            }

            var value = (int)(_health.Value * percents);
            _health.ValueAdd(value);
        }
    }
    public sealed class ToxStatus
    {
            private Health _health;
            private Intoxication _intoxication;

        public EIntoxicationCondition condition = EIntoxicationCondition.Low;
        public ToxStatus(Health health, Intoxication intoxication) 
        {
            _intoxication = intoxication;
            _health = health;
        }

        public void IntoxicatedStatus()
        {

            if (condition == EIntoxicationCondition.Normal)
                    return;
                else
                {

                Intoxication intoxication = new Intoxication(100, _intoxication.Value);
                EffectIntoxicated effect = new EffectIntoxicated(intoxication, _health);
                effect.Update();
                }

        }
    }
    
}
