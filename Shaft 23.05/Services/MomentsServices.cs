using System;
using System.Collections.Generic;

namespace ShaftMechanicalComputing
{
    public class MomentsServices
    {
        public Configurations configurations = new Configurations();

        public float Torque;
        public List<float> BendMoment;
        public List<double> ReductedMoment;

        public MomentsServices()
        {
            BendMoment = new List<float>();
            ReductedMoment = new List<double>();
        }

        public void InitializeConfigurations()
        {
            configurations.SetParameters();
            configurations.ChooseMaterial();
            configurations.SetForces();
        }

        public void ComputeBendMoment()
        {
            float sLength = 0;
            float iloraz = 0;
            float total = 0;
            const int fromMetrToMilimeter = 1000;

            for (int i = 0; i < configurations.forcesProvider.Points.Length - 1; i++)
            {
                if (i == 0)
                {
                    while (sLength <= configurations.forcesProvider.Points[i + 1])
                    {
                        BendMoment.Add((configurations.forcesProvider.ReactionA * sLength) / fromMetrToMilimeter);
                        sLength += configurations.forcesProvider.Lenght / 10;
                    }
                }
                else
                {
                    iloraz += configurations.forcesProvider.AllForces[i] * configurations.forcesProvider.Points[i];
                    total += configurations.forcesProvider.AllForces[i];
                    while (sLength <= configurations.forcesProvider.Points[i + 1])
                    {
                        BendMoment.Add((configurations.forcesProvider.ReactionA * sLength - (total * sLength - iloraz)) / fromMetrToMilimeter);
                        sLength += configurations.forcesProvider.Lenght / 10;
                    }
                }
            }
        }

        public void ComputeTorque()
        {
            const int torqueRatio = 9550;
            Torque = torqueRatio * configurations.forcesProvider.Power / configurations.forcesProvider.Speed;
        }

        public void ComputeReductedMoment()
        {
            for (int i = 0; i < BendMoment.Count; i++)
            {
                ReductedMoment.Add(Math.Sqrt(Torque * Torque + BendMoment[i] * BendMoment[i]));
            }
        }
    }
}