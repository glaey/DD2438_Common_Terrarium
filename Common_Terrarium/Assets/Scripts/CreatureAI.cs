using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// This class is supposed to be inherit to create your DragonAI or KittyAI.
    /// </summary>
    public class CreatureAI : MonoBehaviour
    {
        /// <summary>
        /// The name of the specie. Is supposed to be the same for all the specimens.
        /// Should be setted in the prefab of the specie.
        /// </summary>
        public string specieName;

        /// <summary>
        /// The ID of the specie. Is supposed to be the same for all the specimens.
        /// May be setted by the GameManger when instansiating the creature.
        /// </summary>
        public int specieID;

        private Creature creature;

        /// <summary>
        /// YOU NEED TO OVVERIDE IT IN YOUR DragonAI
        /// This function is called when your creature is within an acceptable
        /// range to eat some food, adapted to your regime of course.
        /// You do not need to necessarily eat it of course.
        /// </summary>
        /// <param name="food"></param>
        public virtual void OnAccessibleFood(GameObject food)
        {
        }

        /// <summary>
        /// YOU NEED TO OVVERIDE IT IN YOUR DragonAI
        /// This function may be called by GameManager to measure the stats of the specie
        /// as number of speciemens and average size. 
        /// </summary>
        public virtual void updateStats()
        {
        }
    }
}