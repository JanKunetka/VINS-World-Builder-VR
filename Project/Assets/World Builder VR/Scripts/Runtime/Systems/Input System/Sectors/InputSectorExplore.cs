using System;
using UnityEngine.InputSystem;

namespace WorldBuilderVR.InputSystem.Sectors
{
    /// <summary>
    /// Handles all inputs for the Explore Mode.
    /// </summary>
    public class InputSectorExplore : IInputSector
    {
        //Left Hand
        public event Action OnCatalogMenuActivate;
        public event Action OnPauseMenuActivate;
        
        //Right Hand
        public event Action<InputAction> OnTeleportActivate;
        public event Action OnTeleportCancel;
        public event Action OnActivatePlacementMode;
        
        private XRInputActions.EMExploreModeLeftHandActions leftHand;
        private XRInputActions.EMExploreModeRightHandActions rightHand;

        public InputSectorExplore(XRInputActions input)
        {
            this.leftHand = input.EMExploreModeLeftHand;
            this.rightHand = input.EMExploreModeRightHand;
        }
        
        /// <summary>
        /// Activates all inputs.
        /// </summary>
        public void Activate()
        {
            leftHand.Enable();
            rightHand.Enable();

            //Left Hand
            leftHand.TeleportModeActivate.performed += WhenTeleportModeActivate;
            leftHand.TeleportModeCancel.performed += WhenTeleportModeCancel;
            leftHand.OpenCatalogMenu.performed += WhenCatalogOpen;
            leftHand.Menu.performed += WhenPauseMenuActivate;
            leftHand.Menu.performed += WhenPauseMenuActivate;
            
            //Right Hand
            rightHand.ActivatePlacementMode.performed += WhenActivatePlacementMode;
        }

        /// <summary>
        /// Deactivates all inputs.
        /// </summary>
        public void Deactivate()
        {
            //Left Hand
            leftHand.TeleportModeActivate.performed -= WhenTeleportModeActivate;
            leftHand.TeleportModeCancel.performed -= WhenTeleportModeCancel;
            leftHand.OpenCatalogMenu.performed -= WhenCatalogOpen;
            leftHand.Menu.performed -= WhenPauseMenuActivate;
            leftHand.Menu.performed -= WhenPauseMenuActivate;
            
            //Right Hand
            rightHand.ActivatePlacementMode.performed += WhenActivatePlacementMode;
            
            leftHand.Disable();
            rightHand.Disable();
        }

        private void WhenCatalogOpen(InputAction.CallbackContext ctx) => OnCatalogMenuActivate?.Invoke();
        private void WhenTeleportModeActivate(InputAction.CallbackContext ctx) => OnTeleportActivate?.Invoke(leftHand.Move);
        private void WhenTeleportModeCancel(InputAction.CallbackContext ctx) => OnTeleportCancel?.Invoke();
        private void WhenPauseMenuActivate(InputAction.CallbackContext ctx) => OnPauseMenuActivate?.Invoke();
        private void WhenActivatePlacementMode(InputAction.CallbackContext ctx) => OnActivatePlacementMode?.Invoke();

    }
}