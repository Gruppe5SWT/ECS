using ECS.Redesign;
using NUnit.Framework;

namespace ECS.Redesign.Test.Unit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            var heater = new FakeHeater();
            var uut = new ECS(23, heater, new FakeTempSensor());
        }

        [Test]
        public void Regulate_TempOverThreshold_HeaterTurnsOff()
        {
            Assert.Pass();
        }
    }
}