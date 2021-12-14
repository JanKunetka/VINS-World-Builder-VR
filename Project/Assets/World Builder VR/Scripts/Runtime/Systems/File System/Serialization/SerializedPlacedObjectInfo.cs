using BoubakProductions.Systems.Serialization;
using UnityEngine;
using WorldBuilderVR.Core;

namespace WorldBuilderVR.Systems.ExternalStorage.Serialization
{
    /// <summary>
    /// The serialized form of the <see cref="PlacedObjectInfo"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedPlacedObjectInfo
    {
        private SerializedVector3 position;
        private SerializedQuaternion rotation;
        private SerializedVector3 scale;

        public SerializedPlacedObjectInfo(PlacedObjectInfo objectInfo) : this(objectInfo.position, objectInfo.rotation, objectInfo.scale) { }
        public SerializedPlacedObjectInfo(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = new SerializedVector3(position);
            this.rotation = new SerializedQuaternion(rotation);
            this.scale = new SerializedVector3(scale);
        }

        /// <summary>
        /// Returns the <see cref="PlacedObjectInfo"/> in a Unity readable format.
        /// </summary>
        /// <returns>The <see cref="PlacedObjectInfo"/>.</returns>
        public PlacedObjectInfo Deserialize()
        {
            PlacedObjectInfo objectInfo;
            objectInfo.position = position.Deserialize();
            objectInfo.rotation = rotation.Deserialize();
            objectInfo.scale = scale.Deserialize();
            return objectInfo;
        }
        
    }
}