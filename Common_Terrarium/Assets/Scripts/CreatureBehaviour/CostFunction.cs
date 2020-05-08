using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CreatureBehaviour
{
    /// <summary>
    /// The cost function simply decides how much energy
    /// is consumed at each deltaTime since the last call
    /// by a creature.
    /// It also describes how much reward one creature gets
    /// for eating something.
    /// </summary>
    public class CostFunction: ICostFunction
    {
        /// <summary>
        /// Computes the amount of energy that a creature consumes for
        /// existing during a time <paramref name="deltaTime"/>.
        /// A cost function can f.e. take into account the character traits of the creature
        /// or simply time without eating...
        /// </summary>
        /// <param name="creature">The creature that will consume energy</param>
        /// <param name="deltaTime">The amount of time since the last call to this function</param>
        /// <returns>The amount of energy the creature has consumed</returns>
        public float LivingCost(Creature creature, float deltaTime)
        {
            return deltaTime * creature.Size * Mathf.Log(1 + creature.MaxSpeed);
        }

        /// <summary>
        /// When the creature moves at a certain speed,
        /// this function returns how much it costs to move.
        /// </summary>
        /// <param name="creature">The moving creature</param>
        /// <param name="speed">The current speed at which it moves</param>
        /// <returns>The cost for moving</returns>
        public float MoveCost(Creature creature, float speed)
        {
            return creature.Size * Mathf.Pow(speed, 2);
        }

        /// <summary>
        /// When the creature eats something, this function will be called
        /// to determine the energy reward(refill) the eating creature receives.
        /// </summary>
        /// <param name="food">The victim of the eating creature, a plant or a herbivore</param>
        /// <param name="regime">Specifies the regime of the creature</param>
        /// <returns>The erngy reward for eating the <paramref name="food"/></returns>
        public float EatingReward(GameObject food, Creature.Regime regime)
        {
            return 0;
        }
    }
}
