using System;
using BoubakProductions.Core;
using UnityEngine;

namespace WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette.TransformOverseer
{
    /// <summary>
    /// Overseers how are position and rotation for the Model Silhouette be calculated.
    /// </summary>
    public class SilhouetteTransformOverseer
    {
        private ModelSilhouette silhouette;

        private const int Amount = 2;
        private ObjectPositionerFree positionerFree;
        private ObjectPositionerSnap positionerSnap;

        private int currentPlacementIndex;

        #region Singleton Pattern
        private static SilhouetteTransformOverseer instance;
        private static readonly object padlock = new object();
        public static SilhouetteTransformOverseer Instance 
        { 
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new SilhouetteTransformOverseer();
                    return instance;
                }
            }
        }
        #endregion
       
        private SilhouetteTransformOverseer() { }
        
        public void Initialize(ModelSilhouette silhouette, InputSatchel playerInputs)
        {
            this.silhouette = silhouette;
            positionerFree = new ObjectPositionerFree(silhouette.TrackedTransform, playerInputs, silhouette.Raycaster);
            positionerSnap = new ObjectPositionerSnap(silhouette.TrackedTransform, playerInputs, silhouette.Raycaster);

            silhouette.OnUpdateTrackedObject += UpdateTrackedObject;
            silhouette.ChangeObjectPositioner(positionerFree);
        }
        
        /// <summary>
        /// Changes to the next placement type.
        /// </summary>
        public void ChangePlacementTypeNext()
        {
            currentPlacementIndex++;
            ChangePlacement();
        }

        /// <summary>
        /// Locks silhouette rotation. 
        /// </summary>
        public void LockRotation()
        {
            positionerFree.LockRotation();
        }

        /// <summary>
        /// Changes the placement type.
        /// </summary>
        /// <exception cref="InvalidOperationException">Is thrown when the index lands on an unsupported number.</exception>
        private void ChangePlacement()
        {
            currentPlacementIndex = IntUtils.Wrap(currentPlacementIndex, 0, Amount-1);
            switch (currentPlacementIndex)
            {
                case 0:
                    silhouette.ChangeObjectPositioner(positionerFree);
                    
                    break;
                case 1:
                    silhouette.ChangeObjectPositioner(positionerSnap);
                    break;
                default:
                    throw new InvalidOperationException("Unsupported Position Placement Type");
            }
        }
        
        /// <summary>
        /// Updates the tracked object in all positioners.
        /// </summary>
        /// <param name="newTransform">The new object to use.</param>
        private void UpdateTrackedObject(Transform newTransform)
        {
            positionerFree.UpdateTrackedObject(newTransform);
            positionerSnap.UpdateTrackedObject(newTransform);
        }

    }
}
