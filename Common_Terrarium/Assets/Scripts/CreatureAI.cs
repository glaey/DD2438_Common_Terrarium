using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// This is the class where most of the work will happen,
    /// like in the previous assignments.
    /// </summary>
    public class CreatureAI : MonoBehaviour
    {
        private Creature creature;

        public void Start()
        {
            Debug.Log($"Creature AI is ready");
            creature = GetComponent<Creature>();
        }

        public void Update()
        {
            //here, you can call creature.Move
            // you can make it sense the surroundings and reproduce, mutation is encompassed in the IReproduction implementation

            /*creature.Move(...)
            *creature.Sensor.SensePreys()
            *creature.Reproduce()
            */

            //Current example :
            var food = creature.Sensor.SensePlants(creature);
            Vector3 closestFood = Vector3.zero;
            float bestDistance = Vector3.Distance(closestFood, transform.position);
            foreach (var foodPiece in food)
            {
                if (Vector3.Distance(foodPiece.transform.position, transform.position) < bestDistance)
                {
                    bestDistance = Vector3.Distance(foodPiece.transform.position, transform.position);
                    closestFood = foodPiece.transform.position;
                }
            }
            if (closestFood != Vector3.zero)
            {
                Debug.DrawLine(transform.position, closestFood, Color.red);
                creature.Move(closestFood - transform.position, 1f);
            } else
            {
                creature.Move(new Vector3(UnityEngine.Random.Range(0f,1f),0,UnityEngine.Random.Range(0f,1f)), 0.2f);
            }

        }

        /// <summary>
        /// This function is called when your creature is within an acceptable
        /// range to eat some food, adapted to your regime of course.
        /// You do not need to necessarily eat it of course.
        /// </summary>
        /// <param name="food"></param>
        public virtual void OnAccessibleFood(GameObject food)
        {
            creature.Eat(food);
            if (creature.Energy > 0.2 * creature.MaxEnergy && UnityEngine.Random.Range(0,1)<0.1f) creature.Reproduce();
        }


    }
}