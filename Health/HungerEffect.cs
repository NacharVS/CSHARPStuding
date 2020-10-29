using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling.Hunger
{
    class HungerEffect
    {
        public enum HungryCondition
        {
            Full,
            Hungry
        }

        private readonly Hunger _hungry;
        private readonly Health _health;
        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(2);
        public Hunger Hungry => _hungry;
        public Health Health => _health;


        public void Update(DateTime current, HungryCondition condition)
        {
            if (Health <= 0)
                return;

            UpdateHungry(current, condition);
        }

        private void UpdateHungry(DateTime current, HungryCondition condition)
        {
          
        }


    }
}
