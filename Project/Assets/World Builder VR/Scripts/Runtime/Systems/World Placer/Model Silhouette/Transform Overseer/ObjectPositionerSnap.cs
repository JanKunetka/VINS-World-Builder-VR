using UnityEngine;
using WorldBuilderVR.Systems.XR;

namespace WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette
{
    /// <summary>
    /// Calculates position and rotation of objects by snapping to surfaces.
    /// </summary>
    public class ObjectPositionerSnap : IObjectPositioner
    {
        private Transform trackedObject;
        private readonly InputSatchel playerInputs;
        private readonly StraightRaycaster raycaster;

        public ObjectPositionerSnap(Transform trackedObject, InputSatchel playerInputs, StraightRaycaster raycaster)
        {
            this.trackedObject = trackedObject;
            this.playerInputs = playerInputs;
            this.raycaster = raycaster;
        }
        
        public Vector3 CalculatePosition()
        {
            return raycaster.HitInfo.point;
        }

        public Quaternion CalculateRotation()
        {
           return Quaternion.FromToRotation(trackedObject.up, raycaster.HitInfo.normal) * trackedObject.rotation;
        }

        public void UpdateTrackedObject(Transform newTransform)
        {
            trackedObject = newTransform;
        }
    }
}