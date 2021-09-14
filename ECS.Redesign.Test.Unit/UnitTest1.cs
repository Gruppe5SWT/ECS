using ECS.Redesign;
using NUnit.Framework;

namespace ECS.Redesign.Test.Unit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            var uut = new ECS(23, new Heater(), new TempSensor());
        }

        [Test]
        public void Regulate_()
        {
            Assert.Pass();
        }
    }
}