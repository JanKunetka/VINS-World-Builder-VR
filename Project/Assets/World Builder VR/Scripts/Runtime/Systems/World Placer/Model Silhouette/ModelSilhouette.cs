using System;
using UnityEngine;
using WorldBuilderVR.Core;
using WorldBuilderVR.Systems.XR;

namespace WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette
{
    /// <summary>
    /// Controls the position, rotation and alignment of the Model Silhouette.
    /// </summary>
    public class ModelSilhouette : MonoBehaviour
    {
        public event Action<Transform> OnUpdateTrackedObject;
        
        [SerializeField] private StraightRaycaster raycaster;
        
        private Transform thisTransform;
        private GameObject currentObject;
        private bool isBlocked;
        
        private IObjectPositioner positioner;

        private void Awake()
        {
            thisTransform = transform;
        }

        private void Update()
        {
            if (currentObject == null) return;
            AlignPosition();
            AlignRotation();   
        }

        /// <summary>
        /// Turns on the silhouette.
        /// </summary>
        public void Enable()
        {
            gameObject.SetActive(true);
            raycaster.BeginScanning();
            raycaster.OnChangeBlockStatus += ChangeBlockedStatus;
        }

        /// <summary>
        /// Turns off the silhouette.
        /// </summary>
        public void Disable()
        {
            raycaster.OnChangeBlockStatus -= ChangeBlockedStatus;
            raycaster.EndScanning();
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Changes the style of object positioning.
        /// </summary>
        /// <param name="objectPositioner">The new style to use.</param>
        public void ChangeObjectPositioner(IObjectPositioner objectPositioner)
        {
            this.positioner = objectPositioner;
        }
        
        /// <summary>
        /// Swap the current silhouette object for a new one.
        /// </summary>
        /// <param name="newSpawnedObject">The new object.</param>
        public void ChangeSpawnedObject(GameObject newSpawnedObject)
        {
            if (currentObject != null) Destroy(currentObject);
            currentObject = Instantiate(newSpawnedObject, thisTransform);
            OnUpdateTrackedObject?.Invoke(currentObject.transform);
            ChangeBlockedStatus(raycaster.HitInfo.isBlocked);
        }

        /// <summary>
        /// Gets the silhouette positional info.
        /// </summary>
        /// <returns>Packed positional info.</returns>
        public PlacedObjectInfo GetTransformInfo()
        {
            return TransformPacker.Pack(thisTransform);
        }
        
        /// <summary>
        /// Aligns Position with the tip of the ray scanner.
        /// </summary>
        private void AlignPosition()
        {
            thisTransform.position = positioner.CalculatePosition();
        }
        
        /// <summary>
        /// Aligns Rotation with the alignTransform.
        /// </summary>
        private void AlignRotation()
        {
            thisTransform.rotation = positioner.CalculateRotation();
        }

        private void ChangeBlockedStatus(bool value)
        {
            currentObject.SetActive(!value);
            isBlocked = value;
        }
        
        public StraightRaycaster Raycaster { get => raycaster; }
        public Transform TrackedTransform { get => thisTransform; }
        public bool IsBlocked { get => isBlocked; }
    }
}