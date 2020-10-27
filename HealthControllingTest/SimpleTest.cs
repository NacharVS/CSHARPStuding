using NUnit.Framework;
using HealthControlling;
using System;

namespace HealthControllingTest
{
    public sealed class Tests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public void Test1()
        {
            Assert.Pass();
            var health = new Health(100);
            var oxygen = new Oxygen(40);
            var effect = new EffectHealthOxygen(oxygen, health);

            var current = new DateTime();
            effect.Update(current, EOxygenCondition.Normal);
            
            current += TimeSpan.FromSeconds(1);
            effect.Update(current, EOxygenCondition.Low);

            current += TimeSpan.FromSeconds(3);
            effect.Update(current, EOxygenCondition.Normal);
        }
    }
}