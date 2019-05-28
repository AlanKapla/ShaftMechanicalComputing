using System;
using System.Collections.Generic;

namespace ShaftMechanicalComputing
{
    public class DiametersServices
    {
        public MomentsServices momentsServices = new MomentsServices();
        public List<double> Diameters = new List<double>();

        public void ComputeDiameter()

        {
            momentsServices.InitializeConfigurations();
            momentsServices.ComputeTorque();
            momentsServices.ComputeBendMoment();
            momentsServices.ComputeReductedMoment();

            for (int i = 0; i < momentsServices.ReductedMoment.Count; i++)
            {
                const int bendingIndicator = 32;
                const float Pi = 3.14f;
                const int safetyRatio = 2;
                const int fromMpaToPa = 1000000;
                const int fromMetrToMilimeter = 1000;
                const float exponent = 0.33f;
                var calculatesDiameter = Math.Pow((momentsServices.ReductedMoment[i] * bendingIndicator) / (Pi * momentsServices.configurations.mechProp / safetyRatio * fromMpaToPa), exponent) * fromMetrToMilimeter;
                Diameters.Add(calculatesDiameter);
            }
        }
    }
}