using System;
using NUnit.Framework;
using HealthControlling;
using HealthControlling.IsRadiation.Source;
using System.Collections;

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

            status.RadSourceList.Add(new RadioactiveArea(1));
            Console.WriteLine("__1__");
            for (int i = 0;i < 10;i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }


            status.RadSourceList.Add(new RadioactiveArea(2));
            Console.WriteLine("__1__2__");
            for (int i = 0; i < 10; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            status.RadSourceList.RemoveAt(0);
            Console.WriteLine("__2__");
            for (int i = 0; i < 40; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            Console.WriteLine("New");

            _health = new Health(100);
            status = new Controller(_health);
            curent = new DateTime();
            status.RadSourceList = new ArrayList();

            status.RadSourceList.Add(new RadioactiveItem(20));
            status.RadSourceList.Add(new RadioactiveItem(30));
            status.RadSourceList.Add(new RadioactiveArea(5));


            Console.WriteLine("__+50__");
            Console.WriteLine("__5__");

            for (int i = 0; i < 5; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            status.RadSourceList.RemoveAt(0);

            Console.WriteLine("__-5__");
            for (int i = 0; i < 25; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            Console.WriteLine("New");

            _health = new Health(100);
            status = new Controller(_health);
            curent = new DateTime();
            status.RadSourceList = new ArrayList();

            status.RadSourceList.Add(new RadioactiveDebuff(2, 10));
            status.RadSourceList.Add(new RadioactiveDebuff(4, 5));

            Console.WriteLine("__+2(10)__+4(5)__");

            for (int i = 0; i < 15; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }
        }
    }
}