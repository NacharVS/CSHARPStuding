using System;
using NUnit.Framework;
using HealthControlling;
using HealthControlling.IsRadiation;

namespace HealthControllingTest
{
    public class SimpleTests
    {
        private Health _health;

        [SetUp]
        public void Setup()
        {
            _health = new Health(100);
        }
        [Test]
        public void Execute()
        {
            Status status = new Status(_health);
            status.StatusUpdate();
            status.RadiationLevel = ERadiationLevel.Hight;
            Console.WriteLine("ERadiationLevel.Hight");
            for (int i = 0;i < 33;i++)
            {
                status.StatusUpdate();
            }

            Console.WriteLine("Check");
            status.RadiationLevel = ERadiationLevel.Low; //Chek: He must take old health, but create new radiation and effect
            Console.WriteLine("ERadiationLevel.Low");
            for (int i = 0; i< 4;i++)
            {
                status.StatusUpdate();
            }

            Console.WriteLine("Refresh");
            _health = new Health(200);
            status = new Status(_health); //Health refresh
            status.RadiationLevel = ERadiationLevel.Low;
            Console.WriteLine("ERadiationLevel.Low");
            for (int i = 0; i < 10; i++)
            {
                status.StatusUpdate();
            }
            status.RadiationLevel = ERadiationLevel.Normal;
            Console.WriteLine("ERadiationLevel.Normal");
            for (int i = 0; i <10; i++)
            {
                status.StatusUpdate();
            }
        }
    }
}