using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IFoodSpawner
    {
        Vector3 SpawnFood(float deltaTime, GameObject[] otherPlants);
    }
}
