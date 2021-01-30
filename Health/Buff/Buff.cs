using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling.Buff
{
    interface IBuff
    {
        public int _duration { get; }

        public void Activate();
        public void Deactivate();

    }
}