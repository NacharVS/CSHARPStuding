using HealthControlling;
using SatietyControlling;
using System;

namespace Baffs
{
    public class HungerBaff : Baff
    {
        private readonly Health _health;
        private readonly Satiety _satiety;

        public HungerBaff(Health health, Satiety satiety)
            : base("Голод", 1, TimeSpan.FromSeconds(6))
        {
            _health = health;
            _satiety = satiety;
        }

        public override void Update(DateTime current)
        {
            if (_satiety.Value != 0)
            {
                Deactivate();
                return;
            }

            _health.Value -= 2;
            base.Update(current);
        }
    }
}
