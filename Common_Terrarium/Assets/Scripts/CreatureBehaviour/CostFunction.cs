using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CreatureBehaviour
{
    /// <inheritdoc cref="ICostFunction"/>
    public class CostFunction: ICostFunction
    {

        public float LivingCost(Creature creature, float deltaTime)
        {
            return deltaTime * creature.Size * Mathf.Log(1 + creature.MaxSpeed);
        }


        public float MoveCost(Creature creature, float speed)
        {
            return creature.Size * Mathf.Pow(speed, 2)/1000;//FIXME : the current /1000 is a temporal fix
        }


        public float EatingReward(GameObject food, Creature.Regime regime)
        {
            if (regime == Creature.Regime.CARNIVORE)
            {
                return food.GetComponent<Creature>().Size;
            } else
            {
                return 1;
            }
        }


        public float ReproductionCost(Creature baby)
        {
            return 1; //FIXME : any better idea ?
        }
    }
}
