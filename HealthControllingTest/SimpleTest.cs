using NUnit.Framework;
using HealthControlling;
using SatietyControlling;
using CharacteristicControllingSystem;
using System;

namespace HealthControllingTest
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            var satiety = new Satiety(100);
            satiety.Value = 100;
            //Assert.IsTrue(satiety.Value == 100);

            var health = new Health(100);
            health.Value = 100;
            //Assert.IsTrue(health.Value == 100);

            var control = new SatietyHealthControl(satiety, health);
            while (health.IsAlive)
            {
                control.Update(DateTime.Now);
                satiety -= 2;
            }
            Assert.IsTrue(health == 0 && satiety <= 0);
        }
    }
}