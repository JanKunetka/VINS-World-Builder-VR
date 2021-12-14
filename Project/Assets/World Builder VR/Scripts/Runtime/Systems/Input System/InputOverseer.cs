using WorldBuilderVR.InputSystem.Registries;
using WorldBuilderVR.InputSystem.Sectors;

namespace WorldBuilderVR.InputSystem
{
    /// <summary>
    /// Handles switching between different Input Action Maps.
    /// </summary>
    public class InputOverseer
    {
        private readonly InputSectorExplore inputExplore;
        private readonly InputSectorPlacement inputPlacement;
        private readonly InputSectorView inputView;

        private readonly InputRegistryExplore registryExplore;
        private readonly InputRegistryPlacement registryPlacement;

        public InputOverseer()
        {
            XRInputActions input = new XRInputActions();
            inputExplore = new InputSectorExplore(input);
            inputPlacement = new InputSectorPlacement(input);
            inputView = new InputSectorView(input);
            registryExplore = InputRegistryExplore.GetInstance();
            registryPlacement = InputRegistryPlacement.GetInstance();
        }

        /// <summary>
        /// Activate the Explore Action Map.
        /// </summary>
        public void ActivateExplore()
        {
            DisableAll();
            inputExplore.Activate();
            registryExplore.Enable(inputExplore);
        }

        /// <summary>
        /// Activate the Placement Action Map.
        /// </summary>
        public void ActivatePlacement()
        {
            DisableAll();
            inputPlacement.Activate();
            registryPlacement.Enable(inputPlacement);
        }

        /// <summary>
        /// Disable all Action Maps.
        /// </summary>
        public void DisableAll()
        {
            inputExplore.Deactivate();
            inputPlacement.Deactivate();
            inputView.Deactivate();
            registryExplore.Disable(inputExplore);
            registryPlacement.Disable(inputPlacement);
        }
    }
}