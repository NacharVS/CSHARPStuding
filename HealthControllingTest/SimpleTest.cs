using System;
using NUnit.Framework;
using HealthControlling;
using HealthControlling.Radiation;

namespace HealthControllingTest
{
    public class SimpleTests
    {
        private Health _health;
        private Radiation _radiation;
        private RadiationEffect _effect;

        [SetUp]
        public void Setup()
        {
            _health = new Health(100);
            _radiation = new Radiation(100);
            _effect = new RadiationEffect(_health, _radiation);
        }
        [Test]
        public void Execute()
        {
            var dateTime = new DateTime();
            for (int i = 0; i < 51; i++)
            {
                Update(dateTime, ERadioationLevel.Low);
            }

            for (int i = 0; i < 21; i++)
            {
                dateTime += TimeSpan.FromSeconds(1);
                Update(dateTime, ERadioationLevel.Normal);
            }

            for (int i = 0; i < 35; i++)
            {
                dateTime += TimeSpan.FromSeconds(2);
                Update(dateTime, ERadioationLevel.Hight);
            }
        }

        private void Update(DateTime dateTime, ERadioationLevel condition)
        {
            _effect.RadiationUpdate(dateTime, condition);

            Console.WriteLine($"health Value:{_health.Value} Max:{_health.Max} ");
            Console.WriteLine($"radiation Value:{_radiation.Value} Max:{_radiation.Max} ");
            Console.WriteLine();
        }
    }
}