using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class UniformFoodSpawner : IFoodSpawner
    {
        private float xMin, xMax, zMin, zMax;
        private float time;
        public UniformFoodSpawner(float xMin, float xMax, float zMin, float zMax)
        {
            this.xMin = xMin;
            this.xMax = xMax;
            this.zMin = zMin;
            this.zMin = zMax;
            time = 0;
        }

        public Vector3 SpawnFood(float deltaTime, GameObject[] otherPlants)
        {
            time += deltaTime;
            if (time > 1)
            {
                time = 0;
                float x = UnityEngine.Random.Range(xMin, xMax);
                float z = UnityEngine.Random.Range(zMin, zMax);
                return new Vector3(x, 0f, z);
            }
            return Vector3.zero;
        }
    }
}
