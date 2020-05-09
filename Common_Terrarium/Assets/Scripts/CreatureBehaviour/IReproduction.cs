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
        /// This reproduction interface allows you to define the characteristics of the newborn.
        /// If you want to make your baby mutate according to some rule of yours,
        /// it's here that it should happen.
        /// </summary>
        /// <param name="parent">The main parent of the newborn</param>
        /// <param name="baby">The baby whose parameters may (or not) be changed</param>
        void CreateBaby(Creature parent,Creature baby);
    }
}
