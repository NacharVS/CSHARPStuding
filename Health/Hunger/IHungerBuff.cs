using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling
{
    interface IHungerBuff
    {
        public void Buff(Hunger hunger,int duration);
        public void Debuf(Hunger hunger, int duration);
    }
}
