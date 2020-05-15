using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.CreatureBehaviour
{
    class AsexualMutationDuplication : IReproduction
    {
        System.Random rand = new System.Random();

        float mutationFactor = 0.2f;

        public void CreateBaby(Creature parent, Creature baby)
        {
            /*
             * This is a demo of how you may finetune your newborn
             * As you may, see mutation happens here !
             */
            baby.CreatureRegime = parent.CreatureRegime;
            baby.Size = parent.Size * ( 1 - mutationFactor/2f + (float)rand.NextDouble() * mutationFactor);
            baby.MaxSpeed = parent.MaxSpeed * (1 - mutationFactor / 2f + (float)rand.NextDouble() * mutationFactor);
            baby.Sensor = new CircularSensorV2(parent.Sensor.SensingRadius * (1 - mutationFactor / 2f + (float)rand.NextDouble() * mutationFactor));
            baby.MaxEnergy = parent.MaxEnergy;
            baby.Generation = parent.Generation + 1;
        }
    }
}
