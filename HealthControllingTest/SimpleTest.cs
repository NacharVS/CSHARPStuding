using System;
using NUnit.Framework;
using HealthControlling;
using HealthControlling.Intoxication;

namespace HealthControllingTest
{
    public class SimpleTests
    {
        private Health _health;
        private Intoxication _intoxication;
        [SetUp]
        public void Setup()
        {
            _health = new Health(100);
            _intoxication = new Intoxication(100,0);
        }

        [Test]
        public void Execute()
        {
            Controller control = new Controller(_health, _intoxication);
            //Assert.IsTrue(_health.Max == 100);
            _intoxication.Value += 75;
            for (int i = 0; i < 150; i++)
            {
                control.IntoxicatedUpdate();
            }

            _intoxication.Value += 70;

            for (int i = 0; i < 150; i++)
            {
                control.IntoxicatedUpdate();
            }

            //dateTime += TimeSpan.FromSeconds(1);
            //Update(dateTime, EIntoxicationCondition.Low);
        }


    }
}