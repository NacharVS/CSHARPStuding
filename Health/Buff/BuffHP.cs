using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HealthControlling.Buff
{
    public class BuffHP
    {
        private int _duration { get; set; }
        private int _valueBuff { get; set; }
        private readonly Health _health;
        public bool IsActivated { get; private set; }
        public BuffHP(Health health)
        {
            _health = health;
        }

        public void Update()
        {
            if (IsActivated)
            {
                _health.ValueAdd(_valueBuff);
                IsActivated = false;
                Console.WriteLine(_health.Value);
            }

                Thread.Sleep(10);
                _duration -= 1;

            if (_duration == 0)
            {
                Deactivate();
                Console.WriteLine("Buff end");
            }
   
        }
        public void Activate(int duration, int valueBuff)
        {
            _duration += duration;
            _valueBuff = valueBuff;
            IsActivated = true;
        }

        public void Deactivate()
        {
            _health.ValueAdd(-_valueBuff);
        }
    }
}
