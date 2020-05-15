using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CreatureBehaviour
{
    /// <summary>
    /// The circular sensor looks all around the creature within a specified radius.
    /// </summary>
    /// <inheritdoc cref="ISensor"/>
    class CircularSensorV2 : ISensorV2
    {

        private float radius;

        /// <summary>
        /// Builds a new sensor.
        /// </summary>
        /// <param name="radius">The maximum radius at which the creature can sense</param>
        public CircularSensorV2(float radius)
        {
            this.radius = radius;
        }

        /// <summary>
        /// <inheritdoc cref="ISensor.SensingRadius"/>
        /// </summary>
        public override float SensingRadius { get => radius; }

        /// <inheritdoc cref="ISensor.SenseCreatures"/>
        public override List<GameObject> SenseCreatures(Creature me)
        {
            List<GameObject> creatures = SenseTag(me, "herbivore");
            creatures.AddRange(SenseTag(me, "carnivore"));

            return creatures;
        }

        /// <inheritdoc cref="ISensor.SensePlants(Creature)"/>
        public override List<GameObject> SensePlants(Creature me)
        {
            return SenseTag(me, "plant");
        }

        /// <inheritdoc cref="ISensor.SensePreys(Creature)"/>
        public override List<GameObject> SensePreys(Creature me)
        {
            //return SenseTag(me, "herbivore");
            var neighbors = SenseCreatures(me);
            var preys = new List<GameObject>();
            foreach (var neighbor in neighbors)
            {
                if (neighbor.GetComponent<Creature>().Size < me.Size * 0.7f)
                    preys.Add(neighbor);
            }
            return preys;
        }

        /// <inheritdoc cref="ISensor.SenseCarnivores(Creature)"/>
        public override List<GameObject> SenseCarnivores(Creature me)
        {
            return SenseTag(me, "carnivore");
        }

        /// <inheritdoc cref="ISensor.SensePredators(Creature)"/>
        public override List<GameObject> SensePredators(Creature me)
        {
            //return SenseTag(me, "herbivore");
            var neighbors = SenseCarnivores(me);

            foreach (var neighbor in neighbors)
            {
                if (neighbor.GetComponent<Creature>().Size * 0.7 < me.Size)
                    neighbors.Remove(neighbor);
            }

            return neighbors;
        }


        private List<GameObject> SenseTag(Creature me, string tag)
        {
            List<GameObject> detectedObjects = new List<GameObject>();

            var allObjects = GameObject.FindGameObjectsWithTag(tag);
            foreach (var obj in allObjects)
            {
                if (Vector3.Distance(me.transform.position, obj.transform.position) <= radius)
                {
                    if (obj.transform != me.transform)
                    {
                        detectedObjects.Add(obj);
                    }
                }
            }
            return detectedObjects;
        }
    }
}
