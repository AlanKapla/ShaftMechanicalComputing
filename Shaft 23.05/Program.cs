using System;

namespace ShaftMechanicalComputing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var diameters = new DiametersServices();

            diameters.ComputeDiameter();

            Console.WriteLine();

            var sLength = 0f;
            for (int i = 0; i < diameters.Diameters.Count; i++)
            {
                Console.WriteLine("For length: " + sLength + "mm" + " " + "d=" + diameters.Diameters[i] + "mm");

                sLength += diameters.momentsServices.configurations.Length / 10;
            }

            Console.ReadKey();
        }
    }
}