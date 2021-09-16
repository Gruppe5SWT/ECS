using ECS.Redesign;
using NUnit.Framework;

namespace ECS.Redesign.Test.Unit
{
    public class Tests
    {
        FakeHeater heater;
        FakeTempSensor tempSensor;
        ECS uut;

        [SetUp]
        public void Setup()
        {
            heater = new FakeHeater();
            tempSensor = new FakeTempSensor();
            uut = new ECS(23, heater, tempSensor);
        }

        [Test]
        public void ECS_InitialiseECS_ThresholdIsExpected()
        {
            Assert.That(uut.GetThreshold().Equals(23));
        }

        [Test]
        public void Regulate_TempUnderThreshold_HeaterTurnsOn()
        {
            tempSensor.Temp = 20;
            uut.Regulate();
            Assert.That(heater.IsOn.Equals(true));
        }

        [Test]
        public void Regulate_TempOverThreshold_HeaterTurnsOff()
        {
            tempSensor.Temp = 25;
            uut.Regulate();
            Assert.That(heater.IsOn.Equals(false));
        }

        [TestCase(21)]
        [TestCase(24)]
        [TestCase(25)]
        [TestCase(27)]
        [TestCase(0)]
        [TestCase(-1)]
        public void SetThreshold_SetNew_GetReturnsExcepted(int temp)
        {
            uut.SetThreshold(temp);
            Assert.That(uut.GetThreshold().Equals(temp));
        }

        [TestCase(21)]
        [TestCase(24)]
        [TestCase(25)]
        [TestCase(27)]
        [TestCase(0)]
        [TestCase(-1)]
        public void GetCurTemp_GetCurrentTemp_TempSensorReturnsExpected(int temp)
        {
            tempSensor.Temp = temp;
            Assert.That(uut.GetCurTemp().Equals(temp));
        }

        [Test]
        public void RunSelfTest_RunTest_TestsReturnTrue()
        {
           Assert.That(uut.RunSelfTest().Equals(true));
        }
    }
}