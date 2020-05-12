using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class TerrainManager : MonoBehaviour
    {
        private IFoodSpawner spawner;
        public GameObject plantFood;
        public GameObject plantSpawn;

        public void Start()
        {
            spawner = new GuassianFoodSpawner(plantSpawn.transform.position, 100);
            //spawner = new UniformFoodSpawner(0, 200, 0, 200);
        }

        public void Update()
        {
            var foodPos = spawner.SpawnFood(Time.deltaTime, GameObject.FindGameObjectsWithTag("plant"));
            if (foodPos != Vector3.zero)
            {
                var newFood = Instantiate(plantFood, foodPos, plantFood.transform.localRotation);
            }
        }
    }
}
