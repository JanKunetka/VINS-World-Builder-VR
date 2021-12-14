using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WorldBuilderVR.InputSystem.Sectors
{
    /// <summary>
    /// Handles all inputs for the Placement Mode.
    /// </summary>
    public class InputSectorPlacement : IInputSector
    {
        //Left Hand
        public event Action OnChangePlacementMode;
        public event Action OnOpenCatalogMenu;
        public event Action OnOrbitObjectsBegin, OnOrbitObjectsEnd;
        public event Action OnLockRotation;
        public event Action OnPauseMenuActivate;
        
        //Right Hand
        public event Action OnDisablePlacementMode;
        public event Action OnPlaceObject;
        public event Action<float> OnDistanceScroll;
        public event Action OnSwitchTool;

        private XRInputActions.EMPlacementModeLeftHandActions leftHand;
        private XRInputActions.EMPlacementModeRightHandActions rightHand;

        public InputSectorPlacement(XRInputActions input)
        {
            this.leftHand = input.EMPlacementModeLeftHand;
            this.rightHand = input.EMPlacementModeRightHand;
        }
        
        /// <summary>
        /// Activates all inputs.
        /// </summary>
        public void Activate()
        {
            leftHand.Enable();
            rightHand.Enable();

            //Left Hand
            leftHand.OpenCatalogMenu.performed += WhenOpenCatalogMenu;
            leftHand.ObjectRotation.started += WhenEnableOrbitBegin;
            leftHand.ObjectRotation.canceled += WhenEnableOrbitEnd;
            leftHand.ResetRotation.performed += WhenLockRotation;
            leftHand.Menu.performed += WhenPauseMenuActivate;
            
            //Right Hand
            rightHand.ChangePlacementType.performed += WhenChangePlacementMode;
            rightHand.DisablePlacementMode.performed += WhenDisablePlacementMode;
            rightHand.PlaceObject.performed += WhenPlaceObject;
            rightHand.DistanceScroll.performed += WhenDistanceScroll;
            rightHand.SwitchTool.performed += WhenSwitchTool;
        }

        /// <summary>
        /// Deactivates all inputs.
        /// </summary>
        public void Deactivate()
        {
            //Left Hand
            leftHand.OpenCatalogMenu.performed -= WhenOpenCatalogMenu;
            leftHand.ObjectRotation.started -= WhenEnableOrbitBegin;
            leftHand.ObjectRotation.canceled -= WhenEnableOrbitEnd;
            leftHand.ResetRotation.performed -= WhenLockRotation;
            leftHand.Menu.performed -= WhenPauseMenuActivate;
            
            //Right Hand
            rightHand.ChangePlacementType.performed -= WhenChangePlacementMode;
            rightHand.DisablePlacementMode.performed -= WhenDisablePlacementMode;
            rightHand.PlaceObject.performed -= WhenPlaceObject;
            rightHand.DistanceScroll.performed -= WhenDistanceScroll;
            rightHand.SwitchTool.performed -= WhenSwitchTool;
            
            leftHand.Disable();
            rightHand.Disable();
        }
        
        //Left Hand
        private void WhenChangePlacementMode(InputAction.CallbackContext ctx) => OnChangePlacementMode?.Invoke();
        private void WhenOpenCatalogMenu(InputAction.CallbackContext ctx) => OnOpenCatalogMenu?.Invoke();
        private void WhenEnableOrbitBegin(InputAction.CallbackContext ctx) => OnOrbitObjectsBegin?.Invoke();
        private void WhenEnableOrbitEnd(InputAction.CallbackContext ctx) => OnOrbitObjectsEnd?.Invoke();
        private void WhenLockRotation(InputAction.CallbackContext ctx) => OnLockRotation?.Invoke();
        private void WhenPauseMenuActivate(InputAction.CallbackContext ctx) => OnPauseMenuActivate?.Invoke();
        
        //Right Hand
        private void WhenDisablePlacementMode(InputAction.CallbackContext ctx) => OnDisablePlacementMode?.Invoke();
        private void WhenPlaceObject(InputAction.CallbackContext ctx) => OnPlaceObject?.Invoke();
        private void WhenDistanceScroll(InputAction.CallbackContext ctx)
        {
            float axis = ctx.ReadValue<Vector2>().y;
            OnDistanceScroll?.Invoke(axis);
        }
        private void WhenSwitchTool(InputAction.CallbackContext ctx) => OnSwitchTool?.Invoke();

    }
}