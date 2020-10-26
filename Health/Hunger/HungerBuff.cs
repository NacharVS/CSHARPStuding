using System;
using System.Collections.Generic;
using System.Text;

namespace HealthControlling
{
    class HungerBuff : IHungerBuff
    {
        private TimeSpan _duration;
        private TimeSpan _update;
        private bool isOver;

        public TimeSpan Duration { get => _duration; set => _duration = value; }
        public TimeSpan Update { get => _update; set => _update = value; }
        public bool IsOver { get => isOver; set => isOver = value; }

        public void Buff(Hunger hunger,int duration)
        {
            this.Duration = new TimeSpan(duration*10000);
            this.Update = new TimeSpan(duration* 10000 / 2);
            while (Duration > TimeSpan.Zero)
            {
                hunger.Max += 10;
                this.Duration.Subtract(new TimeSpan(10000));
            }
        }

        public void Debuf(Hunger hunger, int duration)
        {

            this.Duration = new TimeSpan(duration * 10000);
            this.Update = new TimeSpan(duration * 10000 / 2);
            while (Duration > TimeSpan.Zero)
            {
                hunger.Max -= 10;
                this.Duration.Subtract(new TimeSpan(10000));
            }
        }
    }
}
