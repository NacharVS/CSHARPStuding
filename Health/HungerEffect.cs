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
        private DateTime _lastLowTimeHunger;
        private DateTime _lastLowTimeFull;
        public bool IsEnd { get; private set; }

        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(2);
        public HungerEffect(Hunger hungry, Health health)
        {
            _hungry = hungry;
            _health = health;
        }

        public void Update(DateTime current, HungryCondition condition)
        {
            if (_health < 0)
                return;
            if (_hungry.Value < 0)
            {
                IsEnd = true;
            }

            UpdateHungry(current, condition);
            UpdateHealth();
        }

        private void UpdateHungry(DateTime current, HungryCondition condition)
        {
            double persents = 0.0;
            if (condition == HungryCondition.Hungry)
            {
                _lastLowTimeHunger = current;
              
                TimeSpan deltaFull = _lastLowTimeFull - current ;
                if (deltaFull <= Duration)
                {
                    persents = 0.05;
                }
            }
            else if(condition==HungryCondition.Full )
            {
                _lastLowTimeFull = current;
                TimeSpan deltaHunger = _lastLowTimeHunger - current;
                if (deltaHunger <= Duration)
                {
                    persents = -0.05;
                }
            }
            Console.Write($"Procent of Hunger:{persents} ");
            var value = (int)(_hungry.Max * persents);
            _hungry.ValueAdd(value);

        }
        private void UpdateHealth()
        {
            double percents = 0.0;

            if (_hungry == 0 && _health >0)
                percents = -0.05;
            else if (_hungry == _hungry.Max)
            {
                if (_health < _health.Max)
                    percents = 0.05;
                else
                {
                    Console.Write($"Procent of Health:{percents} \n");
                    return;
                }


            }

            int value = (int)(_health.Value * percents);
            _health.ValueAdd(value);
        }


    }

}
