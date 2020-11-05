using System;
using HealthControlling.Hunger;

namespace HealthControlling.Hunger
{
    class ControllerHunger
    {
        private readonly Health _health;
        private readonly Hunger _hunger;
        public HungerEffect.HungryCondition hungryLevelCondition = HungerEffect.HungryCondition.Full;
        private  HungerEffect _hungerEffect;

        public ControllerHunger(Health health, Hunger hunger)
        {
            _health = health;
            _hunger = hunger;
        }
        public void Update(DateTime dateTime)
        {
            Console.WriteLine($"Seconds: {dateTime.Second}");
            if (hungryLevelCondition != HungerEffect.HungryCondition.Full || _hungerEffect != null)
                UpdateRadiation(hungryLevelCondition);
        }

        public void UpdateRadiation(HungerEffect.HungryCondition level)
        {
            if (_hungerEffect == null)
            {
                _hungerEffect = new HungerEffect(_hunger, _health);
            }

            if (_hungerEffect.IsEnd)
            {
                hungryLevelCondition = HungerEffect.HungryCondition.Full;
                _hungerEffect = null;
                return;
            }
            else if (_hungerEffect != null)
            {
                _hungerEffect.Update();
            }
        }
}
