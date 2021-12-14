using UnityEngine;

namespace WorldBuilderVR.Core
{
    /// <summary>
    /// Contains information about a placed object.
    /// </summary>
    public struct PlacedObjectInfo
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }
}