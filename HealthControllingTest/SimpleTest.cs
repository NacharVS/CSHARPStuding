using System;
using NUnit.Framework;
using HealthControlling;
using HealthControlling.IsRadiation;

namespace HealthControllingTest
{
    public class SimpleTests
    {
        private Health _health;
        private DateTime curent = new DateTime();

        [SetUp]
        public void Setup()
        {
            _health = new Health(100);
        }
        [Test]
        public void Execute()
        {
            Controller status = new Controller(_health);

            curent += TimeSpan.FromSeconds(1);
            status.Update(curent);

            status.RadiationLevel = ERadiationLevel.Hight;
            Console.WriteLine("ERadiationLevel.Hight");
            for (int i = 0;i < 33;i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            Console.WriteLine("Check");

            status.RadiationLevel = ERadiationLevel.Low;
            Console.WriteLine("ERadiationLevel.Low");
            for (int i = 0; i< 4;i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            Console.WriteLine("Refresh");

            _health = new Health(200);
            status = new Controller(_health); //Health refresh
            curent = new DateTime();

            status.RadiationLevel = ERadiationLevel.Low;
            Console.WriteLine("ERadiationLevel.Low");
            for (int i = 0; i < 10; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            status.RadiationLevel = ERadiationLevel.Normal;
            Console.WriteLine("ERadiationLevel.Normal");
            for (int i = 0; i <10; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            Console.WriteLine("UpdateRadiation.Hight");

            status.UpdateRadiation(ERadiationLevel.Hight);
            for (int i = 0; i < 5; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }
        }
    }
}