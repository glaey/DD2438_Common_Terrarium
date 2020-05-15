using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CreatureBehaviour
{
    /// <inheritdoc cref="ICostFunction"/>
    public class CostFunctionV2: ICostFunction
    {

        public float LivingCost(Creature creature, float deltaTime)
        {
            return deltaTime * (creature.Size * Mathf.Log(1 + creature.MaxSpeed) + creature.Sensor.SensingRadius * 0.1f);
        }


        public float MoveCost(Creature creature, float speed)
        {
            return creature.Size * Mathf.Pow(speed, 2) * 0.01f * Time.deltaTime;//FIXME : the current /1000 is a temporal fix
        }


        public float EatingReward(GameObject food, Creature.Regime regime)
        {
            if (regime == Creature.Regime.CARNIVORE)
            {
                return 200 * food.GetComponent<Creature>().Size;
            } else
            {
                return 200;
            }
        }


        public float ReproductionCost(Creature parent)
        {
            return parent.Energy/2;
        }
    }
}
