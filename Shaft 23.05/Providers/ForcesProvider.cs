using System.Collections.Generic;

namespace ShaftMechanicalComputing
{
    public class ForcesProvider
    {
        public string[] MaterialsName = new string[] { "Aluminium PA6", "Steel S235", "Steel C45" };
        public float[] MechanicalProperties = new float[3] { 250f, 235f, 360f };

        public float ReactionA;
        public float ReactionB;
        public float TotalForce;

        public List<float> Forces;
        public List<float> Distances;

        public float[] Points;
        public float[] AllForces;

        public float Power;
        public float Speed;
        public float Lenght;

        public ForcesProvider(float Power, float Speed, float Length)
        {
            Forces = new List<float>();
            Distances = new List<float>();
            this.Power = Power;
            this.Speed = Speed;
            this.Lenght = Length;
        }

        public void AddForces(float force)
        {
            Forces.Add(force);
        }

        public void AddDistances(float distance)
        {
            Distances.Add(distance);
        }

        public float ComputeTotalForce()
        {
            for (int i = 0; i < Forces.Count; i++)
            {
                TotalForce += Forces[i];
            }
            return TotalForce;
        }

        public void ComputeReactionB()
        {
            for (int i = 0; i < Forces.Count; i++)
            {
                ReactionB += ((Forces[i] * Distances[i]) / Lenght);
            }
        }

        public void ComputeReactionA()
        {
            ReactionA = (TotalForce - ReactionB);
        }

        public void CreateForces()
        {
            AllForces = new float[Forces.Count + 2];

            for (int i = 0; i < AllForces.Length; i++)
            {
                if (i == 0)
                {
                    AllForces[i] = ReactionA;
                }
                else if (i == AllForces.Length - 1)
                {
                    AllForces[i] = ReactionB;
                }
                else
                {
                    AllForces[i] = Forces[i - 1];
                }
            }
        }

        public void CreatePoints()
        {
            Points = new float[Distances.Count + 2];

            for (int i = 0; i < Points.Length; i++)
            {
                if (i == 0)
                {
                    Points[i] = 0;
                }
                else if (i == Points.Length - 1)
                {
                    Points[i] = Lenght;
                }
                else
                {
                    Points[i] = Distances[i - 1];
                }
            }
        }
    }
}