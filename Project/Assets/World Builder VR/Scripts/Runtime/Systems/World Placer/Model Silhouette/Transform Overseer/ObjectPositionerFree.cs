using UnityEngine;
using WorldBuilderVR.Systems.XR;

namespace WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette
{
    /// <summary>
    /// Calculates position and rotation for objects in free air.
    /// </summary>
    public class ObjectPositionerFree : IObjectPositioner
    {
        private readonly StraightRaycaster raycaster;
        
        private Transform trackedObject;
        private readonly InputSatchel playerInputs;

        private bool isRotationLocked;

        public ObjectPositionerFree(Transform trackedObject, InputSatchel playerInputs, StraightRaycaster raycaster)
        {
            this.trackedObject = trackedObject;
            this.playerInputs = playerInputs;
            this.raycaster = raycaster;
            LockRotation();
        }

        public Vector3 CalculatePosition()
        {
            Transform pTransform = playerInputs.ReferenceTransform;
            Vector3 freePosition = pTransform.position + pTransform.forward * playerInputs.ScrollRoller.Value;
            float distanceFree = (pTransform.position - freePosition).sqrMagnitude;
            float distanceRay = (raycaster.HitInfo.point - pTransform.position).sqrMagnitude;

            Vector3 targetPosition = distanceRay < distanceFree ? raycaster.HitInfo.point : freePosition;
            return targetPosition;
        }

        public Quaternion CalculateRotation()
        {
            Transform pTransform = playerInputs.ReferenceTransform;
            Vector3 lookRotation = pTransform.position - trackedObject.position;
            lookRotation.y = 0;
            Quaternion targetRotation;

            //Do Rotation based on rotation reference object if button is held down.
            if (playerInputs.OrbitHolder.IsHeld)
            {
                isRotationLocked = false;
                targetRotation = playerInputs.ReferenceOrbitTransform.rotation;
            }
            //Rotate towards player when rotation is locked.
            else if (isRotationLocked)
            {
                targetRotation = Quaternion.LookRotation(lookRotation);
            }
            //Do not rotate.
            else targetRotation = trackedObject.rotation;
            
            return Quaternion.Slerp(trackedObject.rotation, targetRotation, Time.deltaTime * 18);
        }

        public void UpdateTrackedObject(Transform newTransform)
        {
            this.trackedObject = newTransform;
        }

        /// <summary>
        /// Locks the rotation, so the player cannot rotate the object.
        /// </summary>
        public void LockRotation()
        {
            isRotationLocked = true;
        }
    }
}