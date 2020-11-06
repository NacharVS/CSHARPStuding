using System;
using NUnit.Framework;
using HealthControlling;
using HealthControlling.IsRadiation;
using HealthControlling.IsRadiation.Source;

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

            status.RadSourceList.Add(new RadioactiveWater());
            Console.WriteLine("ERadiationLevel.Low");
            for (int i = 0;i < 10;i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            status.RadSourceList.Add(new RadioactiveWater());
            Console.WriteLine("ERadiationLevel.Hight");
            for (int i = 0; i < 30; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            Console.WriteLine("Check");

            status.RadSourceList.Add(new RadioactiveWater());
            Console.WriteLine("ERadiationLevel.Low");
            for (int i = 0; i< 4;i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            Console.WriteLine("Refresh");

            _health = new Health(200);
            status = new Controller(_health);
            curent = new DateTime();

            status.RadSourceList.Add(new RadioactiveArea());
            Console.WriteLine("ERadiationLevel.Hight");
            for (int i = 0; i < 10; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            status.RadSourceList.RemoveAt(0);
            Console.WriteLine("ERadiationLevel.Normal");
            for (int i = 0; i < 10; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }
        }
    }
}