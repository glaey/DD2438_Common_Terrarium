using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {

        public float edibleRadius;
        public TerrainManager terrainManager;
        public CreatureAI[] species;
        public GameObject[] spawnAreas;
        public int nIndividualsPerSpecies;

        public void Awake()
        {
            SpawnCreatures();
        }

        public void Update()
        {
            //satisfy the herbivores
            foreach(var animal in GameObject.FindGameObjectsWithTag("herbivore"))
            {
                Creature c = animal.GetComponent<Creature>();
                List<GameObject> animalFood;
                
                // Herbivores eat plants
                animalFood = c.Sensor.SensePlants(c);

                //the closest within the edible radius can be eaten
                GameObject closestFood=null;
                float distance = float.MaxValue;
                foreach(var foodPiece in animalFood)
                {
                    float localDistance = Vector3.Distance(animal.transform.position, foodPiece.transform.position);
                    if (localDistance<distance && localDistance < edibleRadius)
                    {
                        distance = localDistance;
                        closestFood = foodPiece;
                    }
                }
                if (closestFood != null)
                {
                    animal.GetComponent<CreatureAI>().OnAccessibleFood(closestFood);
                    Debug.Log($"Calling the eating method !");
                }

            }
            //same with carnivores
            foreach (var animal in GameObject.FindGameObjectsWithTag("carnivore"))
            {
                Creature c = animal.GetComponent<Creature>();
                List<GameObject> animalFood;

                //carnivores eat herbivores
                animalFood = c.Sensor.SensePreys(c);


                //the closest within the edible radius can be eaten
                GameObject closestFood = null;
                float distance = float.MaxValue;
                foreach (var foodPiece in animalFood)
                {
                    float localDistance = Vector3.Distance(animal.transform.position, foodPiece.transform.position);
                    if (localDistance < distance && localDistance < edibleRadius)
                    {
                        distance = localDistance;
                        closestFood = foodPiece;
                    }
                }
                if (closestFood != null)
                {
                    animal.GetComponent<CreatureAI>().OnAccessibleFood(closestFood);
                    Debug.Log($"Calling the eating method !");
                }

            }
        }

        // Reset for ml agents
        public void ResetEnvironment()
        {
            EndEpisodes();
            DestroyCreatures();
            DestroyPlants();
            SpawnCreatures();
        }

        private void EndEpisodes()
        {
            GameObject[] herb = GameObject.FindGameObjectsWithTag("herbivore");
            GameObject[] carn = GameObject.FindGameObjectsWithTag("carnivore");
            for (var i = 0; i < herb.Length; i++)
            {
                CreatureAI c = herb[i].GetComponent<CreatureAI>();
               // c.EndMe();
            }
            for (var i = 0; i < carn.Length; i++)
            {
                CreatureAI c = carn[i].GetComponent<CreatureAI>();
                //c.EndMe();
            }
        }

        private void SpawnCreatures()
        {
            //int n = species.Length * nIndividualsPerSpecies;
            int n = nIndividualsPerSpecies;

            for (int k = 0; k < species.Length; k++)
            {
                for (int i = 0; i < nIndividualsPerSpecies; i++)
                {
                    Debug.Log($"Creating species {k} - creature {i}");
                    //float angle = (k * nIndividualsPerSpecies + i) * 360f / n;
                    float angle = (nIndividualsPerSpecies + i) * 360f / n;
                    Vector3 position = spawnAreas[k].transform.position + Quaternion.Euler(0, angle, 0f) * Vector3.forward * 50;
                    position.y = 15f;
                    Instantiate(species[k], position, new Quaternion(0, angle, 0, 0));
                }
            }
        }

        private void DestroyPlants()
        {
            GameObject[] plants = GameObject.FindGameObjectsWithTag("plant");
            for (var i = 0; i < plants.Length; i++)
            {
                Destroy(plants[i]);
            }
        }

        private void DestroyCreatures()
        {
            GameObject[] herb = GameObject.FindGameObjectsWithTag("herbivore");
            GameObject[] carn = GameObject.FindGameObjectsWithTag("carnivore");
            for (var i = 0; i < herb.Length; i++)
            {
                Destroy(herb[i]);
            }
            for (var i = 0; i < carn.Length; i++)
            {
                Destroy(carn[i]);
            }
        }

    }
}
