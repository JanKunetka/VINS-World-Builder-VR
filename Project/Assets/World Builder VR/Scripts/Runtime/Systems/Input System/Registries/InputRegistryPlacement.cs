using BoubakProductions.Core;
using UnityEngine;
using WorldBuilderVR.Editors.Core;
using WorldBuilderVR.InputSystem.Sectors;
using WorldBuilderVR.Systems.WorldPlacer;
using WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette.TransformOverseer;
using WorldBuilderVR.User;

namespace WorldBuilderVR.InputSystem.Registries
{
    /// <summary>
    /// Registers all relevant methods to input actions.
    /// </summary>
    public class InputRegistryPlacement : MonoSingleton<InputRegistryPlacement>
    {
        [SerializeField] private MenuVisibilityController catalogMenuController;
        [SerializeField] private MenuVisibilityController pauseMenuController;
        [SerializeField] private RollerValueAction distanceScrollRoller;
        [SerializeField] private HoldValueAction orbitHolder;

        /// <summary>
        /// Subscribe input to systems.
        /// </summary>
        /// <param name="sector">The sector containing proper input system information.</param>
        public void Enable(InputSectorPlacement sector)
        {
            //Left Hand
            sector.OnOpenCatalogMenu += catalogMenuController.SwitchMenuStatus;
            sector.OnChangePlacementMode += WorldPlacerOverseer.Instance.ChangePlacementType;
            sector.OnOrbitObjectsBegin += orbitHolder.Begin;
            sector.OnOrbitObjectsEnd += orbitHolder.End;
            sector.OnLockRotation += SilhouetteTransformOverseer.Instance.LockRotation;
            sector.OnPauseMenuActivate += pauseMenuController.SwitchMenuStatus;
            
            //Right Hand
            sector.OnDisablePlacementMode += EditModeOverseer.Instance.SwitchModeToExplore;
            sector.OnPlaceObject += WorldPlacerOverseer.Instance.ApplyToolEffect;
            sector.OnDistanceScroll += distanceScrollRoller.Recalculate;
            sector.OnSwitchTool += WorldPlacerOverseer.Instance.CycleTool;
        }
        
        /// <summary>
        /// Unsubscribe input to systems.
        /// </summary>
        /// <param name="sector">The sector containing proper input system information.</param>
        public void Disable(InputSectorPlacement sector)
        {
            //Left Hand
            sector.OnOpenCatalogMenu -= catalogMenuController.SwitchMenuStatus;
            sector.OnChangePlacementMode -= WorldPlacerOverseer.Instance.ChangePlacementType;
            sector.OnOrbitObjectsBegin -= orbitHolder.Begin;
            sector.OnOrbitObjectsEnd -= orbitHolder.End;
            sector.OnLockRotation -= SilhouetteTransformOverseer.Instance.LockRotation;
            sector.OnPauseMenuActivate -= pauseMenuController.SwitchMenuStatus;
            
            //Right Hand
            sector.OnDisablePlacementMode -= EditModeOverseer.Instance.SwitchModeToExplore;
            sector.OnPlaceObject -= WorldPlacerOverseer.Instance.ApplyToolEffect;
            sector.OnDistanceScroll -= distanceScrollRoller.Recalculate;
            sector.OnSwitchTool -= WorldPlacerOverseer.Instance.CycleTool;
        }
    }
}