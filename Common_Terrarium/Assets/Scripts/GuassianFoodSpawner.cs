using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class GuassianFoodSpawner : IFoodSpawner
    {
        private float xMin, xMax, zMin, zMax;
        private float time;
        public float nr_guassians = 10;
        public float std = 20;
        private List<(float, float)> centers;
        public GuassianFoodSpawner(float xMin, float xMax, float zMin, float zMax)
        {
            this.xMin = xMin;
            this.xMax = xMax;
            this.zMin = zMin;
            this.zMax = zMax;
            time = 0;

            centers = new List<(float, float)>();
            for(int i = 0; i < nr_guassians; i++)
            {
                float x = UnityEngine.Random.Range(xMin, xMax);
                float z = UnityEngine.Random.Range(zMin, zMax);
                centers.Add((x, z));
                //Debug.Log("center at: " + x + " " + z);
            }
        }

        public Vector3 SpawnFood(float deltaTime, GameObject[] otherPlants)
        {
            time += deltaTime;
            if (time > 1)
            {
                time = 0;

                int guassian = UnityEngine.Random.Range(0, centers.Count);
                float x_mean = centers[guassian].Item1;
                float z_mean = centers[guassian].Item2;

                System.Random rand = new System.Random();
                double x_u1 = 1.0f - rand.NextDouble();
                double x_u2 = 1.0f - rand.NextDouble();
                double x_randStdNormal = Math.Sqrt(-2.0 * Math.Log(x_u1)) * Math.Sin(2.0 * Math.PI * x_u2);

                double z_u1 = 1.0f - rand.NextDouble();
                double z_u2 = 1.0f - rand.NextDouble();
                double z_randStdNormal = Math.Sqrt(-2.0 * Math.Log(z_u1)) * Math.Sin(2.0 * Math.PI * z_u2);

                float x = x_mean + std * (float)x_randStdNormal;
                float z = z_mean + std * (float)z_randStdNormal;

                if (x > xMax) x = xMax;
                if (x < xMin) x = xMin;

                if (z > zMax) z = zMax;
                if (z < zMin) z = zMin;

                //Debug.DrawLine(new Vector3(x_mean,0.1f, z_mean), new Vector3(x, 0.1f, z), Color.white, 300f);

                return new Vector3(x, 0f, z);
            }
            return Vector3.zero;
        }
    }
}
