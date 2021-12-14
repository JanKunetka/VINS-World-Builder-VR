using UnityEngine;

namespace WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette
{
    public interface IObjectPositioner
    {
        /// <summary>
        /// Calculates new position for the tracked object.
        /// </summary>
        /// <returns></returns>
        Vector3 CalculatePosition();
        
        /// <summary>
        /// Calculates new rotation for the tracked object.
        /// </summary>
        /// <returns></returns>
        Quaternion CalculateRotation();

        /// <summary>
        /// Updates the tracked object to a new one.
        /// </summary>
        /// <param name="newTransform">The new Tracked Object.</param>
        void UpdateTrackedObject(Transform newTransform);
    }
}