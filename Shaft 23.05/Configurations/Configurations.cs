using System;

namespace ShaftMechanicalComputing
{
    public class Configurations
    {
        public float Power;
        public float Speed;
        public float Length;
        public float mechProp = 0;

        public ForcesProvider forcesProvider;

        public void SetParameters()
        {
            Console.WriteLine("Enter motor power in [kW]");
            Power = float.Parse(Console.ReadLine());

            Console.WriteLine("Enter motor speed in [RPM]");
            Speed = float.Parse(Console.ReadLine());

            Console.WriteLine("Enter shaft length in [mm]");
            Length = float.Parse(Console.ReadLine());

            forcesProvider = new ForcesProvider(Power, Speed, Length);
        }

        public void ChooseMaterial()
        {
            Console.WriteLine("\nChoose material for shaft");

            string choosenMat = "";

            for (int i = 0; i < forcesProvider.MaterialsName.Length; i++)
            {
                Console.WriteLine((i + 1) + ": " + forcesProvider.MaterialsName[i]);
            }

            var choose = Console.ReadLine();

            for (int i = 1; i <= forcesProvider.MaterialsName.Length; i++)
            {
                if (choose == Convert.ToString(i))
                {
                    mechProp = forcesProvider.MechanicalProperties[i - 1];
                    choosenMat = forcesProvider.MaterialsName[i - 1];
                }
            }

            Console.WriteLine("\nChoosen material: " + choosenMat + ": " + "Re=" + mechProp + "MPa");
        }

        public void SetForces()
        {
            Console.WriteLine("\nPlease enter force and distance, if you want to break please eneter'stop'");

            string force;
            for (; ; )
            {
                Console.WriteLine("Enter force in [N]:");
                force = Console.ReadLine();
                if (force != "stop")
                {
                    forcesProvider.AddForces(float.Parse(force));

                    Console.WriteLine("Enter distance from start of XY in [mm]:");
                    forcesProvider.AddDistances(float.Parse(Console.ReadLine()));
                }
                else
                {
                    break;
                }
            }

            forcesProvider.ComputeTotalForce();
            forcesProvider.ComputeReactionB();
            forcesProvider.ComputeReactionA();
            forcesProvider.CreateForces();
            forcesProvider.CreatePoints();
        }
    }
}