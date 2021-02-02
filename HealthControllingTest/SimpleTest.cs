using System;
using NUnit.Framework;
using HealthControlling;
using HealthControlling.Intoxication;
using HealthControlling.Buff;

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
            BuffHP buff = new BuffHP(_health);
            //Assert.IsTrue(_health.Max == 100);
            buff.Activate(30, 100);

            for (int i = 0; i < 150; i++)
            {
                //control.IntoxicatedUpdate();
                buff.Update();
            }

            //_intoxication.Value += 70;

            //for (int i = 0; i < 150; i++)
            //{
            //    control.IntoxicatedUpdate();
              
            //}

            //dateTime += TimeSpan.FromSeconds(1);
            //Update(dateTime, EIntoxicationCondition.Low);
        }


    }
}