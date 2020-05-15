using System;
using System.IO;
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
    public class ChickenAI : CreatureAI
    {
        private Creature creature;

        System.Random rand;

        // stats of the specie
        static int nOfSpeciemens;
        static float avgSize;


        public void Start()
        {
            creature = GetComponent<Creature>();

            rand = new System.Random();

        }

        public void Update()
        {
            Vector3 dir = Vector3.zero;
            float speed = 1;

            List<GameObject> predators = creature.Sensor.SensePredators(creature);
            List<GameObject> food = creature.Sensor.SensePlants(creature);

            if (predators.Count > 0)
            {
                // run away from the predator
                dir = (transform.position - getClosest(predators).transform.position).normalized;
            }
            else if (food.Count > 0 && creature.Energy<creature.MaxEnergy*0.8)
            {
                // can see food, go and take it
                dir = (getClosest(food).transform.position - transform.position).normalized;
                speed = 0.6f;
            }
            else
            {
                // cannot see any food, explore
                dir = unexploredDirection();
                speed = 0.5f;
            }

            creature.Move(dir, speed);


            // Debug.Log("dir=" + dir + " speed=" +speed);
        }

        public override void OnAccessibleFood(GameObject food)
        {
            creature.Eat(food);
            if (UnityEngine.Random.Range(0, creature.MaxEnergy) < creature.Energy) creature.Reproduce();
        }


        Vector3 unexploredDirection()
        {
            Vector3 dir = new Vector3(rand.Next(-10, 10), 0, rand.Next(-10, 10));
            dir = dir * 0.0001f;

            return dir.normalized;
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, creature.Sensor.SensingRadius);
            //drawExplorationMap();
        }


        GameObject getClosest(List<GameObject> set)
        {
            GameObject closest = set[0];

            foreach(GameObject obj in set)
            {
                if (Vector3.Distance(obj.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position))
                    closest = obj;
            }

            return closest;
        }

        public override void updateStats()
        {
            List<GameObject> agents = GameObject.FindGameObjectsWithTag("carnivore").ToList();
            agents.AddRange(GameObject.FindGameObjectsWithTag("herbivore").ToList());

            agents = agents.FindAll(c => c.GetComponent<CreatureAI>().specieID == specieID);

            nOfSpeciemens = agents.Count;

            avgSize = 0;


            foreach (GameObject agent in agents)
            {
                Creature c = agent.GetComponent<Creature>();

                avgSize += c.Size;
            }

            avgSize = avgSize / (float)nOfSpeciemens;

            Debug.Log(specieName + " " + nOfSpeciemens + " avgSize=" + avgSize);
        }
    }
}