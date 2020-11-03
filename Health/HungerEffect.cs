using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling.Hunger
{
    public class HungerEffect
    {
        public enum HungryCondition
        {
            Full,
            Hungry
        }

        private readonly Hunger _hungry;
        private readonly Health _health;
        private DateTime _lastLowTime;
        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(2);
        public HungerEffect(Hunger hungry, Health health)
        {
            _hungry = hungry;
            _health = health;
        }

        public void Update(DateTime current, HungryCondition condition)
        {
            if (_health <= 0)
                return;

            UpdateHungry(current, condition);
            UpdateHealth(condition);
        }

        private void UpdateHungry(DateTime current, HungryCondition condition)
        {
            double persents = 0.0;
            if (condition == HungryCondition.Hungry)
            {
                _lastLowTime = current;
            }
            else
            {
                TimeSpan delta = current - _lastLowTime;
                if (delta <= Duration)
                {
                    persents = -0.05;
                }
            }
            Console.Write($"Процент голода:{persents} ");
            var value = (int)(_hungry.Max * persents);
            _hungry.ValueAdd(value);

        }
        private void UpdateHealth(HungryCondition condition)
        {
            double percents = 0.0;

            if (condition == HungryCondition.Hungry)
                percents = -0.05;
            else if (condition == HungryCondition.Full)
            {
                if (_health < _health.Max)
                    percents = 0.01;
                else
                {
                    Console.Write($"Процент жизни:{percents} \n");
                    return;
                }


            }

            var value = (int)(_health.Value * percents);
            _health.ValueAdd(value);
        }


    }

}

