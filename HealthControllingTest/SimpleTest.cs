using System;
using NUnit.Framework;
using HealthControlling;
using HealthControlling.IsRadiation;

namespace HealthControllingTest
{
    public class SimpleTests
    {
        private Health _health;
        //private Radiation _radiation;
        //private RadiationEffect _effect;

        [SetUp]
        public void Setup()
        {
            _health = new Health(100);
            //_radiation = new Radiation(100);
            //_effect = new RadiationEffect(_health, _radiation);
        }
        [Test]
        public void Execute()
        {
            Status status = new Status(_health);
            status.RadiationLevel = ERadiationLevel.Low;
            for (int i = 0;i < 50;i++)
            {
                status.StatusUpdate();
            }
        }
        //[Test]
        //public void Execute()
        //{
        //    //var dateTime = new DateTime();
        //    for (int i = 0; i < 51; i++)
        //    {
        //        Update(/*dateTime,*/ ERadiationLevel.Low);
        //    }

        //    for (int i = 0; i < 21; i++)
        //    {
        //        //dateTime += TimeSpan.FromSeconds(1);
        //        Update(/*dateTime,*/ ERadiationLevel.Normal);
        //    }

        //    for (int i = 0; i < 35; i++)
        //    {
        //        //dateTime += TimeSpan.FromSeconds(2);
        //        Update(/*dateTime,*/ ERadiationLevel.Hight);
        //    }
        //}

        //private void Update(/*DateTime dateTime,*/ ERadiationLevel condition)
        //{
        //    //_effect.RadiationUpdate(/*dateTime,*/ condition);

        //    Console.WriteLine($"health Value:{_health.Value} Max:{_health.Max} ");
        //    Console.WriteLine($"radiation Value:{_radiation.Value} Max:{_radiation.Max} ");
        //    Console.WriteLine();
        //}
    }
}