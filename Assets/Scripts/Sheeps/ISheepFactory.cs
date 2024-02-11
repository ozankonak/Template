using UnityEngine;

namespace Sheeps
{
    public interface ISheepFactory 
    {
        Sheep CreateSheep(Vector3 position, Quaternion rotation);

        void AutoSpawn();
    }
}
