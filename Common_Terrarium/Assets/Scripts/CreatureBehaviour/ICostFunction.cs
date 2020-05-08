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
    public interface ICostFunction
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
        float LivingCost(Creature creature, float deltaTime);

        /// <summary>
        /// When the creature moves at a certain speed,
        /// this function returns how much it costs to move.
        /// </summary>
        /// <param name="creature">The moving creature</param>
        /// <param name="speed">The current speed at which it moves</param>
        /// <returns>The cost for moving</returns>
        float MoveCost(Creature creature, float speed);

        /// <summary>
        /// When the creature eats something, this function will be called
        /// to determine the energy reward(refill) the eating creature receives.
        /// </summary>
        /// <param name="food">The victim of the eating creature, a plant or a herbivore</param>
        /// <param name="regime">Specifies the regime of the creature</param>
        /// <returns>The erngy reward for eating the <paramref name="food"/></returns>
        float EatingReward(GameObject food, Creature.Regime regime);
    }
}
