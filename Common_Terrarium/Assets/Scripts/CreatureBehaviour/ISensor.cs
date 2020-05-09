using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CreatureBehaviour
{
    /// <summary>
    /// The goal of a sensor is to detect other gameobjects that are
    /// interesting for the creature.
    /// One can think of circular sensing : detecting everyone within a radius
    /// Or think of cone sensing : I can see until infinity between -theta/+theta in front of me
    /// </summary>
    public abstract class ISensor
    {
        /// <summary>
        /// Detects ALL living creatures that are in your neighborhood, i.e herbivores and carnivores
        /// </summary>
        /// <param name="me">The sensing creature</param>
        /// <returns>A List of GameObject containing all surrounding living creatures.
        /// To access their properties, use <code>gameObject.GetComponent<Creature>()</code></returns>
        public abstract List<GameObject> SenseCreatures(Creature me);

        /// <summary>
        /// Detects all preys, i.e. herbivores, in your neighborhood.
        /// </summary>
        /// <param name="me">The sensing creature</param>
        /// <returns>A List of GameObject containing all surrounding herbivorous creatures.
        /// To access their properties, use <code>gameObject.GetComponent<Creature>()</code></returns>
        public abstract List<GameObject> SensePreys(Creature me);

        /// <summary>
        /// Detects all edible plants in your neighborhood
        /// </summary>
        /// <param name="me">The sensing creature</param>
        /// <returns>A List of GameObject containing all surrounding .
        /// To access their properties, use <code>gameObject.GetComponent<Creature>()</code></returns>
        public abstract List<GameObject> SensePlants(Creature me);

        public abstract float SensingRadius { get; }
    }
}
