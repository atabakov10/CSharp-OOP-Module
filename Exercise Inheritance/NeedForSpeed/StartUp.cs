using System;

namespace NeedForSpeed
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            CrossMotorcycle sportCar = new CrossMotorcycle(100, 100);
            sportCar.Drive(9);
            Console.WriteLine(sportCar.Fuel);
        }
    }
}
