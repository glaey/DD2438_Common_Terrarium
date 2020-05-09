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
    class CircularSensor : ISensor
    {

        private float radius;

        /// <summary>
        /// Builds a new sensor.
        /// </summary>
        /// <param name="radius">The maximum radius at which the creature can sense</param>
        public CircularSensor(float radius)
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
            return SenseTag(me,"creature");
        }

        /// <inheritdoc cref="ISensor.SensePlants(Creature)"/>
        public override List<GameObject> SensePlants(Creature me)
        {
            return SenseTag(me, "plant");
        }

        /// <inheritdoc cref="ISensor.SensePreys(Creature)"/>
        public override List<GameObject> SensePreys(Creature me)
        {
            var neighbors = SenseCreatures(me);
            var herbivorePreys = new List<GameObject>();
            foreach(var neighbor in neighbors)
            {
                if (neighbor.GetComponent<Creature>().CreatureRegime == Creature.Regime.HERBIVORE)
                    herbivorePreys.Add(neighbor);
            }
            return herbivorePreys;
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
