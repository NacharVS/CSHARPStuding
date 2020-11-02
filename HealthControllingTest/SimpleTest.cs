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
            _intoxication = new Intoxication(100, 50);

        }

        [Test]
        public void Execute()
        {
            Controller control = new Controller(_health, _intoxication);
            Assert.IsTrue(_health.Max == 100);


            for (int i = 0; i < 50; i++)
            {
                control.IntoxicatedStatus();
            }


            //dateTime += TimeSpan.FromSeconds(1);
            //Update(dateTime, EIntoxicationCondition.Low);
        }


    }
}