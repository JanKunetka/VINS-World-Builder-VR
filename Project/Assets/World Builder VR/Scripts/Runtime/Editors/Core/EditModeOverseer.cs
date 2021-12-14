using UnityEngine;
using WorldBuilderVR.Editors.Models;
using WorldBuilderVR.InputSystem;
using WorldBuilderVR.Systems.WorldPlacer;

namespace WorldBuilderVR.Editors.Core
{
    /// <summary>
    /// Overseers everything happening in Edit Mode.
    /// </summary>
    public class EditModeOverseer
    {
        private InputOverseer input;
        private readonly WorldPlacerOverseer worldPlacer;
        
        //TODO Instead of holding a single model pack, this overseer should hold an entire World Pack.
        private ModelPackAsset currentPack;
        private ModeType currentMode;

        #region Singleton Pattern
        private static EditModeOverseer instance;
        private static readonly object padlock = new object();
        public static EditModeOverseer Instance 
        { 
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new EditModeOverseer();
                    return instance;
                }
            }
        }
        #endregion

        private EditModeOverseer()
        {
            worldPlacer = WorldPlacerOverseer.Instance;
            currentMode = ModeType.None;
        }

        /// <summary>
        /// Assigns a model pack to Edit Mode.
        /// </summary>
        /// <param name="modelPack">The model pack to use.</param>
        public void AssignAsset(ModelPackAsset modelPack)
        {
            input = new InputOverseer();
            
            currentPack = modelPack;
            SwitchModeToExplore();
        }
        
        #region Switch Modes

        public void SwitchModeToNone()
        {
            currentMode = ModeType.None;
            input.DisableAll();
            
            worldPlacer.DisablePlacementMode();
        }
        
        /// <summary>
        /// Switches the current mode to Exploration, A Mode where you can walk around.
        /// </summary>
        public void SwitchModeToExplore()
        {
            currentMode = ModeType.Explore;
            input.ActivateExplore();
            
            worldPlacer.DisablePlacementMode();
        }
        
        public void SwitchModeToPlacement()
        {
            SwitchModeToPlacement(0);
            worldPlacer.CycleTool();
        }
        /// <summary>
        /// Switches the current mode to Placement, a Mode where you can spawn, place and rotate objects.
        /// </summary>
        public void SwitchModeToPlacement(int index)
        {
            currentMode = ModeType.Placement;
            input.ActivatePlacement();
            
            worldPlacer.AssignAsset(currentPack.Models[index]);
        }

        #endregion
        
        public ModelPackAsset CurrentPack
        {
            get
            {
                if (this.currentPack == null) throw new MissingReferenceException("The Current Model Pack is null. Did you forget to activate the editor?");
                return this.currentPack;
            }
        }
        public ModeType CurrentMode { get => currentMode; }
    }
}