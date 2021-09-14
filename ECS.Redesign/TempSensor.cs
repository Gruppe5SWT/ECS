using System;

namespace ECS.Redesign
{
    public class TempSensor : ITempSensor
    {
        private Random gen = new Random();

        public int GetTemp()
        {
            return gen.Next(-5, 45);
        }

        public bool RunSelfTest()
        {
            return true;
        }
    }

    public class FakeTempSensor : ITempSensor
    {
        private int temp { get; set; } = 20;

        public int GetTemp()
        {
            return temp;
        }

        public bool RunSelfTest()
        {
            return true;
        }
    }

    public interface ITempSensor
    {
        int GetTemp();
        bool RunSelfTest();
    }
}