using System;
using BoubakProductions.Safety;
using WorldBuilderVR.Editors.Models;
using WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette;
using WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette.TransformOverseer;
using WorldBuilderVR.Systems.WorldPlacer.ToolBox;
using WorldBuilderVR.Systems.XR;

namespace WorldBuilderVR.Systems.WorldPlacer
{
    /// <summary>
    /// Overseers the activity of placing things in the world.
    /// </summary>
    public class WorldPlacerOverseer
    {
        public event Action OnDisablePlacementMode;
        
        private ModelAsset currentAsset;
        
        private ModelSilhouette silhouette;
        private SilhouetteTransformOverseer silhouetteTransformOverseer;
        private ToolboxManager toolbox;
        
        #region Singleton Pattern
        private static WorldPlacerOverseer instance;
        private static readonly object padlock = new object();

        public static WorldPlacerOverseer Instance 
        { 
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new WorldPlacerOverseer();
                    return instance;
                }
            }
        }
        #endregion

        private WorldPlacerOverseer() { }

        public void Initialize(ModelSilhouette modelSilhouette, IRaycaster toolRaycaster)
        {
            this.silhouette = modelSilhouette;
            this.silhouette.gameObject.SetActive(false);
            this.silhouetteTransformOverseer = SilhouetteTransformOverseer.Instance;

            this.toolbox = new ToolboxManager(toolRaycaster, modelSilhouette);
        }
        
        /// <summary>
        /// Assigns a new active asset and initiates the Object Placement Mode.
        /// </summary>
        /// <param name="asset">The new asset to spawn.</param>
        public void AssignAsset(ModelAsset asset)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned Asset");
            
            currentAsset = asset;
            silhouette.ChangeSpawnedObject(asset.Model);
            toolbox.Refresh(currentAsset);
            toolbox.ChooseFirst();
            
            EnablePlacementMode();
        }

        /// <summary>
        /// Enables the Object Placement Mode.
        /// </summary>
        public void EnablePlacementMode()
        {
            silhouette.Enable();
        }
        
        /// <summary>
        /// Disables the Object Placement Mode.
        /// </summary>
        public void DisablePlacementMode()
        {
            silhouette.Disable();
            currentAsset = null;
            OnDisablePlacementMode?.Invoke();
        }

        /// <summary>
        /// Changes the placement type for Object Silhouette.
        /// </summary>
        public void ChangePlacementType()
        {
            silhouetteTransformOverseer.ChangePlacementTypeNext();    
        }
        
        /// <summary>
        /// Initiates the process of placing the object/model.
        /// </summary>
        public void ApplyToolEffect()
        {
            toolbox.ApplyEffect();
        }

        /// <summary>
        /// Switches from the current tool to the next one.
        /// </summary>
        public void CycleTool()
        {
            toolbox.Cycle();
        }

        public ModelAsset CurrentAsset { get => currentAsset; }
    }
}