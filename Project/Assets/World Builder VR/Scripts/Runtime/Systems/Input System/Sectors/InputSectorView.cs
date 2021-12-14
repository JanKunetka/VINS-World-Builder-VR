namespace WorldBuilderVR.InputSystem.Sectors
{
    public class InputSectorView : IInputSector
    {
        private readonly XRInputActions input;
        
        public InputSectorView(XRInputActions input)
        {
            this.input = input;
        }
        
        public void Activate()
        {
            input.XRILeftHandView.Enable();
            input.XRIRightHandView.Enable();
        }

        public void Deactivate()
        {
            input.XRILeftHandView.Disable();
            input.XRIRightHandView.Disable();
        }
    }
}