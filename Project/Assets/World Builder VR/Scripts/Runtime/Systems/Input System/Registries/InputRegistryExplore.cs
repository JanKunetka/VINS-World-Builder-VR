using BoubakProductions.Core;
using UnityEngine;
using WorldBuilderVR.Editors.Core;
using WorldBuilderVR.InputSystem.Sectors;
using WorldBuilderVR.User;

namespace WorldBuilderVR.InputSystem.Registries
{
    /// <summary>
    /// Registers all relevant methods to input actions.
    /// </summary>
    public class InputRegistryExplore : MonoSingleton<InputRegistryExplore>
    {
        [SerializeField] private TeleportationOverseer teleportOverseer;
        [SerializeField] private MenuVisibilityController catalogMenuController;
        [SerializeField] private MenuVisibilityController pauseMenuController;
        
        /// <summary>
        /// Subscribe input to systems.
        /// </summary>
        /// <param name="sector">The sector containing proper input system information.</param>
        public void Enable(InputSectorExplore sector)
        {
            //Left Hand
            sector.OnCatalogMenuActivate += catalogMenuController.SwitchMenuStatus;
            sector.OnPauseMenuActivate += pauseMenuController.SwitchMenuStatus;
            
            //Right Hand
            sector.OnActivatePlacementMode += EditModeOverseer.Instance.SwitchModeToPlacement;
            
            //Teleport Action - disabled, because app does not have any sorts of Options Menu.
            // sector.OnTeleportActivate += teleportOverseer.WhenTeleportActivate;
            // sector.OnTeleportCancel += teleportOverseer.WhenTeleportCancel;
        }
        
        /// <summary>
        /// Unsubscribe input to systems.
        /// </summary>
        /// <param name="sector">The sector containing proper input system information.</param>
        public void Disable(InputSectorExplore sector)
        {
            //Left Hand
            sector.OnCatalogMenuActivate -= catalogMenuController.SwitchMenuStatus;
            sector.OnPauseMenuActivate -= pauseMenuController.SwitchMenuStatus;
            
            //Right Hand
            sector.OnActivatePlacementMode -= EditModeOverseer.Instance.SwitchModeToPlacement;
            
            //Teleport Action - disabled, because app does not have any sorts of Options Menu.
            // sector.OnTeleportActivate -= teleportOverseer.WhenTeleportActivate;
            // sector.OnTeleportCancel -= teleportOverseer.WhenTeleportCancel;
        }
        
        public MenuVisibilityController PauseMenuController { get => pauseMenuController; } 
    }
}