namespace WorldBuilderVR.Systems.XR
{
    /// <summary>
    /// A Base for all wannabe raycast classes.
    /// </summary>
    public interface IRaycaster
    {
        /// <summary>
        /// Begins the raycast scanning.
        /// </summary>
        void BeginScanning();

        /// <summary>
        /// Stops the Raycast Scanning.
        /// </summary>
        void EndScanning();
        
        public HitInfo HitInfo { get; }
        
    }
}