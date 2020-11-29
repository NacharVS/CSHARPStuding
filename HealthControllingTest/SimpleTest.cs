using System;
using NUnit.Framework;
using HealthControlling;
using HealthControlling.IsRadiation.Source;
using System.Collections;
using HealthControlling.IsRadiation;

namespace HealthControllingTest
{
    public class SimpleTests
    {
        private Health _health;
        private DateTime curent = new DateTime();
        private RadEffectConstant _radEffectConstant;

        [SetUp]
        public void Setup()
        {
            _health = new Health(100);
            _radEffectConstant = new RadEffectConstant(0.8, 0.65, 0.5, 0.1, 5, 25, 50, 75, 95, 2);
        }
        [Test]
        public void Execute()
        {
            Controller status = new Controller(_health, _radEffectConstant);

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

            status.RadSourceList.Add(new RadioactiveArea(3));
            Console.WriteLine("__2__3__");
            for (int i = 0; i < 22; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            Console.WriteLine("New");

            _health = new Health(100);
            status = new Controller(_health, _radEffectConstant);
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
            for (int i = 0; i < 16; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }

            Console.WriteLine("New");

            _health = new Health(100);
            status = new Controller(_health, _radEffectConstant);
            curent = new DateTime();
            status.RadSourceList = new ArrayList();

            status.RadSourceList.Add(new RadioactiveDebuff(2, 10));
            status.RadSourceList.Add(new RadioactiveDebuff(4, 5));

            Console.WriteLine("__+2(10)__+4(5)__");

            for (int i = 0; i < 19; i++)
            {
                curent += TimeSpan.FromSeconds(1);
                status.Update(curent);
            }
        }
    }
}