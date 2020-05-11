using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

namespace Assets.Scripts
{
    /// <summary>
    /// This is the class where most of the work will happen,
    /// like in the previous assignments.
    /// </summary>
    public class CreatureAI : MonoBehaviour
    {
        private Creature creature;
        private RayPerceptionSensorComponent3D raySensors;

        public void Start()
        {
            Debug.Log($"Creature AI is ready");
            creature = GetComponent<Creature>();

            // TODO: match raycast length to sensor size
            RayPerceptionSensorComponent3D raySensors = GetComponent<RayPerceptionSensorComponent3D>();

            raySensors.rayLength = creature.Sensor.SensingRadius;
        }

        //public void Update()
        //{
        //    //here, you can call creature.Move
        //    // you can make it sense the surroundings and reproduce, mutation is encompassed in the IReproduction implementation

        //    /*creature.Move(...)
        //    *creature.Sensor.SensePreys()
        //    *creature.Reproduce()
        //    */

        //    //Current example :
        //    var food = creature.Sensor.SensePlants(creature);
        //    Vector3 closestFood = Vector3.zero;
        //    float bestDistance = Vector3.Distance(closestFood, transform.position);
        //    foreach (var foodPiece in food)
        //    {
        //        if (Vector3.Distance(foodPiece.transform.position, transform.position) < bestDistance)
        //        {
        //            bestDistance = Vector3.Distance(foodPiece.transform.position, transform.position);
        //            closestFood = foodPiece.transform.position;
        //        }
        //    }
        //    if (closestFood != Vector3.zero)
        //    {
        //        Debug.DrawLine(transform.position, closestFood, Color.red);
        //        creature.Move(closestFood - transform.position, 1f);
        //    }
        //    //Vector3 dir = new Vector3(0.1f, 0f, 0.2f);
        //    //creature.Move(dir, 1f);
        //}

        private (GameObject, bool) FindFood()
        {
            List<GameObject> food = creature.Sensor.SensePlants(creature);
            Vector3 closestFood = Vector3.zero;
            float bestDistance = Vector3.Distance(closestFood, transform.position);
            GameObject closestFoodObj = new GameObject("empty");
            if (food.Count == 0)
                return ((closestFoodObj, false));
            foreach (var foodPiece in food)
            {
                if (Vector3.Distance(foodPiece.transform.position, transform.position) < bestDistance)
                {
                    bestDistance = Vector3.Distance(foodPiece.transform.position, transform.position);
                    closestFood = foodPiece.transform.position;
                    closestFoodObj = foodPiece;
                }
            }
            return (closestFoodObj, true);
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
        }

        //// Perform actions based on a vector of numbers
        //public override void OnActionReceived(float[] vectorAction)
        //{
            
        //    float horiz = vectorAction[0];
        //    float vert = vectorAction[1];
        //    float speed = vectorAction[2];
        //    float eat = vectorAction[3];
        //    float reproduction = vectorAction[4];

        //    // Direction, normalized
        //    if (horiz == 2f)
        //        horiz = -1f;
        //    if (vert == 2f)
        //        vert = -1f;
        //    Vector3 dir = new Vector3(horiz, 0f, vert);
        //    dir = dir.normalized;

        //    // Speed, normalized
        //    speed = speed / 4f;

        //    // Eat
        //    if (eat == 1f)
        //    {
        //        // Try to eat
        //        // TODO: Return empty object if no food, and check if empty
        //        (GameObject, bool) food = FindFood();
        //        if (food.Item2)
        //        {
        //            creature.Eat(food.Item1);
        //            AddReward(1.0f);
        //        }
        //    }
                
        //    // Reproduce
        //    bool repSuccess = false;
        //    // Try to reproduce
        //    //if (reproduction == 1f)
        //    //    repSuccess = creature.Reproduce();
        //    // If successful, add reward
        //    if (repSuccess)
        //        AddReward(0.5f);

        //    creature.Move(dir, speed);

        //    AddReward(-1f/3000f);
        //}

        //// Reset the agent and area
        //public override void OnEpisodeBegin()
        //{

        //}

        //public override void CollectObservations(VectorSensor sensor)
        //{
        //    // Add parameters
            
        //    // 1 value, INT
        //    sensor.AddObservation((int)creature.CreatureRegime);

        //    // 1 value, float
        //    sensor.AddObservation(creature.Energy);

        //    // 1 value, float
        //    sensor.AddObservation(creature.MaxEnergy);

        //    // 1 value, float
        //    sensor.AddObservation(creature.Size);

        //    // 4 VALUES TOTAL

        //}

        //public void EndMe()
        //{
        //    EndEpisode();
        //}
    }
}