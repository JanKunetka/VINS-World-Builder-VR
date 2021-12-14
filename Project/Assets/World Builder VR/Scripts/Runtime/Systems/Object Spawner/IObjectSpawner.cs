using UnityEngine;
using WorldBuilderVR.Core;

namespace WorldBuilderVR.Systems.ObjectSpawning
{
    /// <summary>
    /// A base for all classes handling object spawning.
    /// </summary>
    public interface IObjectSpawner
    {
        /// <summary>
        /// Spawn an object.
        /// </summary>
        /// <param name="spawnObject">The object to spawn.</param>
        /// <param name="objectInfo">The objects transform info.</param>
        void Spawn(GameObject spawnObject, PlacedObjectInfo objectInfo);
    }
}