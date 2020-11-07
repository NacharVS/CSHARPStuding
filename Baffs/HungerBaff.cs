using HealthControlling;
using SatietyControlling;
using System;
using System.Runtime.InteropServices.ComTypes;

namespace Baffs
{
    public class HungerBaff
    {
        private readonly Health _health;
        private readonly Satiety _satiety;
        public BaffTimer timer;
        public HungerBaff(Health health, Satiety satiety)
        {
            _health = health;
            _satiety = satiety;
            timer = new BaffTimer(TimeSpan.FromSeconds(5));
        }

        public void Update(DateTime current)
        {
            if (_satiety.Value != 0 || timer==null)
            {
                return;
            }
            _health.Value -= 2;
            timer.Update(current);
        }
       

    }
}
