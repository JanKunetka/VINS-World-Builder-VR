using UnityEngine;

namespace WorldBuilderVR.InputSystem
{
    /// <summary>
    /// Stores a value for detecting continuous button hold.
    /// </summary>
    public class HoldValueAction : MonoBehaviour
    {
        private bool isHeld;

        public void Begin()
        {
            isHeld = true;
        }

        public void End()
        {
            isHeld = false;
        }
        
        public bool IsHeld { get => isHeld; }
    }
}