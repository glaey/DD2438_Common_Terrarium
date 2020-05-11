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
        public int nIndividualsPerSpecies;

        public void Awake()
        {
            int n = species.Length * nIndividualsPerSpecies;
            
            for(int k=0; k<species.Length;k++)
            {
                for(int i=0; i < nIndividualsPerSpecies; i++)
                {
                    Debug.Log($"Creating species {k} - creature {i}");
                    float angle = (k * nIndividualsPerSpecies + i) * 360f / n;
                    Vector3 position = transform.position + Quaternion.Euler(0, angle, 0f) * Vector3.forward *50 ;
                    Instantiate(species[k], position, new Quaternion(0,angle,0,0));
                }
            }
        }

        public void Update()
        {
            //satisfy the herbivores
            foreach(var animal in GameObject.FindGameObjectsWithTag("creature"))
            {
                Creature c = animal.GetComponent<Creature>();
                List<GameObject> animalFood;

                if (c.CreatureRegime == Creature.Regime.HERBIVORE)
                {
                    //herbivores eat plants
                    animalFood = c.Sensor.SensePlants(c);
                } else
                {
                    //carnivores eat herbivores
                    animalFood = c.Sensor.SensePreys(c);
                }

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
        }

    }
}
