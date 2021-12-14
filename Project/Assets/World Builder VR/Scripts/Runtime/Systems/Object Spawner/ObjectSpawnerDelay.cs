using UnityEngine;
using WorldBuilderVR.Core;

namespace WorldBuilderVR.Systems.ObjectSpawning
{
    /// <summary>
    /// Spawns GameObjects.
    /// </summary>
    public class ObjectSpawnerDelay : MonoBehaviour, IObjectSpawner
    {
        [SerializeField] private float cooldownTime;
        private float cooldown;

        public ObjectSpawnerDelay(float cooldown)
        {
            this.cooldownTime = cooldown;
        }

        /// <summary>
        /// Spawns a object only after some cooldown.
        /// </summary>
        /// <param name="spawnObject">The object to spawn.</param>
        /// <param name="objectInfo">The object's positional info.</param>
        public void SpawnWithCooldown(GameObject spawnObject, PlacedObjectInfo objectInfo)
        {
            if (cooldown > Time.time) return;
            Spawn(spawnObject, objectInfo);
            cooldown = Time.time + cooldownTime;
        }

        public void Spawn(GameObject spawnObject, PlacedObjectInfo objectInfo)
        {
            Transform spawnedObject = Instantiate(spawnObject, objectInfo.position, objectInfo.rotation).transform;
            spawnedObject.localScale = objectInfo.scale;
        }
    }
}