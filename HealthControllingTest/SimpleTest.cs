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
            var health = new Health(100);
            var oxygen = new Oxygen(100);
            var controller = new Controller(oxygen, health);
            var current = new DateTime();
            controller.Update(current);

            for(var i = 1; i < 110; i++)
            {
                if(i < 10)
                {
                    controller.OxygenCondition = EOxygenCondition.Low;
                }    
                else if (i < 20)
                {
                    controller.OxygenCondition = EOxygenCondition.Normal;
                }
                else if (i < 35)
                {
                    controller.OxygenCondition = EOxygenCondition.Low;
                }
                else if (i < 60)
                {
                    controller.OxygenCondition = EOxygenCondition.Normal;
                }
                else if (i < 100)
                {
                    controller.OxygenCondition = EOxygenCondition.Low;
                }
                else
                {
                    controller.OxygenCondition = EOxygenCondition.Normal;
                }
                current += TimeSpan.FromSeconds(1);
                controller.Update(current);
                //Thread.Sleep(300);
            }
        }
    }
}
