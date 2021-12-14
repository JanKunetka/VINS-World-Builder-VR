namespace WorldBuilderVR.InputSystem.Sectors
{
    /// <summary>
    /// A Base for all Input Sectors.
    /// </summary>
    public interface IInputSector
    {
        /// <summary>
        /// Activates all inputs.
        /// </summary>
        void Activate();

        /// <summary>
        /// Deactivates all inputs.
        /// </summary>
        void Deactivate();
    }
}