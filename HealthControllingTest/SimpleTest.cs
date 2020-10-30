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
        private EffectIntoxicated _effect;

        [SetUp]
        public void Setup()
        {
            _health = new Health(100);
            _intoxication = new Intoxication { Max = 100 };
            _effect = new EffectIntoxicated(_intoxication, _health);
        }

        [Test]
        public void Execute()
        {
            var dateTime = new DateTime();
            Update(dateTime, EIntoxicationCondition.Low);

            Assert.IsTrue(_health.Max == 100);
            Assert.IsTrue(_health.Value == 9999);

            dateTime += TimeSpan.FromSeconds(1);
            Update(dateTime, EIntoxicationCondition.Low);

            dateTime += TimeSpan.FromSeconds(2);
            Update(dateTime, EIntoxicationCondition.Low);
        }

        private void Update(DateTime dateTime, EIntoxicationCondition condition)
        {
            _effect.Update(dateTime, condition);

            Console.WriteLine($"health Value:{_health.Value} Max:{_health.Max} ");
            Console.WriteLine($"intoxication Value:{_intoxication.Value} Max:{_intoxication.Max} ");
        }
    }
}