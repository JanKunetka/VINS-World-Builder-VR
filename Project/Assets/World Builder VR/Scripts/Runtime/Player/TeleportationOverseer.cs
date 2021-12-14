using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace WorldBuilderVR.User
{
    /// <summary>
    /// Handles the player teleportation movement type.
    /// </summary>
    public class TeleportationOverseer : MonoBehaviour
    {
        [SerializeField] private TeleportationProvider provider;
        [SerializeField] private XRRayInteractor rayInteractor;
        
        private bool isActive;
        private InputAction leftStick;

        private void Awake()
        {
            SwitchTeleportStatus(false);
        }

        private void Update()
        {
            DoTeleportation();
        }

        private void DoTeleportation()
        {
            //Is Teleportation active?
            if (!isActive) return;
            
            //Is the player not holding the joystick?
            if (leftStick.triggered) return;

            //Is the 3D Ray hitting anything?
            if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                SwitchTeleportStatus(false);
                return;
            }

            //Create a teleport request
            TeleportRequest request = new TeleportRequest();
            request.destinationPosition = hit.point;
            
            //Execute Teleport Request
            provider.QueueTeleportRequest(request);
            SwitchTeleportStatus(false);
        }
        
        public void WhenTeleportActivate(InputAction joystick)
        {
            leftStick = joystick;
            SwitchTeleportStatus(true);
        }
        
        public void WhenTeleportCancel()
        {
            SwitchTeleportStatus(false);
        }

        private void SwitchTeleportStatus(bool status)
        {
            rayInteractor.enabled = status;
            isActive = status;
        }
        
    }
}