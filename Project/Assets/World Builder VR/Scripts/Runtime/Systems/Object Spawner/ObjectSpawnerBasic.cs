using UnityEngine;
using WorldBuilderVR.Core;

namespace WorldBuilderVR.Systems.ObjectSpawning
{
    /// <summary>
    /// A basic Object Spawner that is not a MonoBehaviour.
    /// </summary>
    public class ObjectSpawnerBasic : IObjectSpawner
    {
        public void Spawn(GameObject spawnObject, PlacedObjectInfo objectInfo)
        {
            GameObject gObject = Object.Instantiate(spawnObject, objectInfo.position, objectInfo.rotation);
            gObject.transform.localScale = objectInfo.scale;
        }
    }
}