using NUnit.Framework;
using HealthControlling;
using HealthControlling.Hunger;
using System;
using System.Threading;

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
            var health = new Health(100);
            var hungry = new Hunger(100);
            var effect = new HungerEffect(hungry, health);
            ShowValue(hungry, health, 0);
            var current = new DateTime();
            for(int i = 1; i < 110; i++)
            {
                if (i < 50)
                {
                    effect.Update(current, HungerEffect.HungryCondition.Full);
                }
                else if (i > 50)
                {
                    effect.Update(current, HungerEffect.HungryCondition.Hungry);
                }
                ShowValue(hungry, health, i);
                current += TimeSpan.FromSeconds(1);
                //Thread.Sleep(300);
            }
        }
        
        void ShowValue(Hunger hunger, Health health, int time)
        {
            Console.WriteLine($"Голод {hunger.Value}  Здоровье: { health.Value} Время {time}\n");
        }
    }
}
