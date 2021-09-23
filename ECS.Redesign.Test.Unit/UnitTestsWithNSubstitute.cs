using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Redesign.Test.Unit
{
    public class UnitTestsWithNSubstitute
    {
        IHeater heater;
        ITempSensor tempSensor;
        ECS uut;

        [SetUp]
        public void Setup()
        {
            heater = Substitute.For<IHeater>();
            tempSensor = Substitute.For<ITempSensor>();
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
            tempSensor.GetTemp().Returns(20);
            uut.Regulate();
            heater.Received(1).TurnOn();
        }

        [Test]
        public void Regulate_TempOverThreshold_HeaterTurnsOff()
        {
            tempSensor.GetTemp().Returns(25);
            uut.Regulate();
            heater.Received(1).TurnOff();
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
            tempSensor.GetTemp().Returns(temp);
            Assert.That(uut.GetCurTemp().Equals(temp));
        }

        [Test]
        public void RunSelfTest_RunTest_TestsReturnTrue()
        {
            tempSensor.RunSelfTest().Returns(true);
            heater.RunSelfTest().Returns(true);
            Assert.That(uut.RunSelfTest().Equals(true));
        }

        [Test]
        public void RunSelfTest_RunTest_TestsReturnFalse()
        {
            tempSensor.RunSelfTest().Returns(false);
            heater.RunSelfTest().Returns(true);
            Assert.That(uut.RunSelfTest().Equals(false));
        }

        [Test]
        public void Regulate_HeaterOffTemperatureGoesLow_HeaterTurnsOn()
        {
            heater.TurnOff();
            tempSensor.GetTemp().Returns(20);
            uut.Regulate();

            heater.Received(1).TurnOn();
            heater.Received(1).TurnOff();
        }
    }
}
