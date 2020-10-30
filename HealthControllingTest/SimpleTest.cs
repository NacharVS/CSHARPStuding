using NUnit.Framework;
using HealthControlling;
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
            /*
             * Все работает, на столько хорошо, что даже если хп 0, потом он не прибавляет,
             * типо умер. Хотя я этого не делал. Не знаю даже рабоваться или нет.
             * 
             */

            var health = new Health(100);
            var oxygen = new Oxygen(100);
            var effect = new EffectHealthOxygen(oxygen, health);
            ShowValue(oxygen, health, 0);
            var current = new DateTime();
            for(int i = 1; i < 110; i++)
            {
                if(i < 10)
                {
                    effect.Update(current, EOxygenCondition.Low);
                }    
                else if (i < 20)
                {
                    effect.Update(current, EOxygenCondition.Normal);
                }
                else if (i < 35)
                {
                    effect.Update(current, EOxygenCondition.Low);
                }
                else if (i < 60)
                {
                    effect.Update(current, EOxygenCondition.Normal);
                }
                else if (i < 100)
                {
                    effect.Update(current, EOxygenCondition.Low);
                }
                else
                {
                    effect.Update(current, EOxygenCondition.Normal);
                }
                ShowValue(oxygen, health, i);
                current += TimeSpan.FromSeconds(1);
                //Thread.Sleep(300);
            }
        }
        
        void ShowValue(Oxygen oxygen, Health health, int time)
        {
            Console.WriteLine($"Кислород: {oxygen.Value}  Здоровье: { health.Value} ({time} с.)\n");
        }
    }
}