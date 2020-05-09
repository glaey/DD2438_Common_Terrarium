using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.CreatureBehaviour
{
    class AsexualCommonDuplication : IReproduction
    {
        public void CreateBaby(Creature parent, Creature baby)
        {
            /*
             * This is a demo of how you may finetune your newborn
             * As you may, see mutation happens here !
             */
            baby.Size = parent.Size;
            baby.MaxSpeed = parent.MaxSpeed;
            baby.CreatureRegime = parent.CreatureRegime;
            baby.Sensor = new CircularSensor(parent.Sensor.SensingRadius);
            baby.MaxEnergy = parent.MaxEnergy;
        }
    }
}
