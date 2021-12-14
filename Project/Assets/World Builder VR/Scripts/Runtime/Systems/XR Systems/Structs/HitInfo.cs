using UnityEngine;

namespace WorldBuilderVR.Systems.XR
{
    /// <summary>
    /// Contains data, that a Raycaster should be able to send to other objects.
    /// </summary>
    public struct HitInfo
    {
        public Transform transform;
        public Vector3 point;
        public Vector3 normal;
        public bool isBlocked;
    }
}