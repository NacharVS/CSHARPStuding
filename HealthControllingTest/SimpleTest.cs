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
            Satiety heroSatiety = new Satiety(100);
            heroSatiety.Value = 110;
            Health heroHealth = new Health(100);
            heroHealth.Value = 110;
            SatietyHealthControl heroControl = new SatietyHealthControl(heroSatiety, heroHealth);
            while (heroHealth.IsAlive)
            {
                heroControl.Update();
                heroSatiety -= 2;
            }
            Assert.IsTrue(heroHealth == 0 && heroSatiety == 0);
        }
    }
}