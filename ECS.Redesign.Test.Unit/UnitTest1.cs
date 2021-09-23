using ECS.Redesign;
using NUnit.Framework;
using NSubstitute;

namespace ECS.Redesign.Test.Unit
{
    public class Tests
    {
        //FakeHeater heater;
        //FakeTempSensor tempSensor;
        private IHeater _fakeHeater;
        private ITempSensor _fakeTempsensor;
        private ECS _uut;

        [SetUp]
        public void Setup()
        {
            //heater = new FakeHeater();
            //tempSensor = new FakeTempSensor();
            _fakeHeater = Substitute.For<IHeater>();
            _fakeTempsensor = Substitute.For<ITempSensor>();
            _uut = new ECS(23, _fakeHeater, _fakeTempsensor);
        }

        [Test]
        public void ECS_InitialiseECS_ThresholdIsExpected()
        {
            Assert.That(_uut.GetThreshold().Equals(23));
        }

        [Test]
        public void Regulate_TempUnderThreshold_HeaterTurnsOn()
        {
            _fakeTempsensor.GetTemp().Returns(20);
            
            _uut.Regulate();

            _fakeHeater.Received(1).TurnOn();
        }

        [Test]
        public void Regulate_TempOverThreshold_HeaterTurnsOff()
        {
            _fakeTempsensor.GetTemp().Returns(25);
            _uut.Regulate();

            _fakeHeater.Received(1).TurnOff();
        }

        [TestCase(21)]
        [TestCase(24)]
        [TestCase(25)]
        [TestCase(27)]
        [TestCase(0)]
        [TestCase(-1)]
        public void SetThreshold_SetNew_GetReturnsExcepted(int temp)
        {
            _uut.SetThreshold(temp);
            Assert.That(_uut.GetThreshold().Equals(temp));
        }

        [TestCase(21)]
        [TestCase(24)]
        [TestCase(25)]
        [TestCase(27)]
        [TestCase(0)]
        [TestCase(-1)]
        public void GetCurTemp_GetCurrentTemp_TempSensorReturnsExpected(int temp)
        {
            _fakeTempsensor.GetTemp().Returns(temp);


            Assert.That(_uut.GetCurTemp().Equals(temp));
        }

        [Test]
        public void RunSelfTest_RunTest_TestsReturnTrue()
        {
           _fakeHeater.RunSelfTest().Returns(true);
           _fakeTempsensor.RunSelfTest().Returns(true);

           Assert.That(_uut.RunSelfTest().Equals(true));
        }
    }
}