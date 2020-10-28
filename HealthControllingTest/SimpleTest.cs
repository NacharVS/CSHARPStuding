using NUnit.Framework;
using HealthControlling;
using System;

namespace HealthControllingTest
{
    public sealed class SimpleTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Execute()
        {
            Assert.Pass();
            var health = new Health(100);
            var oxygen = new Oxygen(40);
            var effect = new EffectHealthOxygen(oxygen, health);

            var current = new DateTime();
            effect.Update(current, EOxygenCondition.Normal);
            ShowValue(oxygen, health);

            current += TimeSpan.FromSeconds(1);
            effect.Update(current, EOxygenCondition.Low);
            ShowValue(oxygen, health);

            current += TimeSpan.FromSeconds(3);
            effect.Update(current, EOxygenCondition.Normal);
            ShowValue(oxygen, health);
        }

        void ShowValue(Oxygen oxygen, Health health)
        {
            Console.WriteLine($"Êטסכמנמה: {oxygen.Value}  Çהמנמגüו: { health.Value}");
        }
    }
}