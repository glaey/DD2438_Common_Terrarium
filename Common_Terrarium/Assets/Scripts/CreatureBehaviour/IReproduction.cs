using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.CreatureBehaviour
{
    /// <summary>
    /// Describes how an individual creature is able to reproduce
    /// </summary>
    public interface IReproduction
    {
        /// <summary>
        /// Should instantiate a new creature near the parent one.
        /// Even though the interface looks like asexual reproduction,
        /// remember you can use the sensor of the parent to detect the closest similar
        /// individual and then do bisexual reproduction.
        /// </summary>
        /// <param name="parent">The main parent of the newborn</param>
        /// <returns>The amount of energy that is spent into reproducing</returns>
        float CreateBaby(Creature parent);
    }
}
