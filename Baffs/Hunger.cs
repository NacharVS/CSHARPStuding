using HealthControlling;
using SatietyControlling;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baffs
{
    public class Hunger:Baff
    {
        Health _health;
        Satiety _satiety;
        public Hunger(Health health) : base("Голод", 1, TimeSpan.FromSeconds(6))
        {
            
            _health = health;
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
