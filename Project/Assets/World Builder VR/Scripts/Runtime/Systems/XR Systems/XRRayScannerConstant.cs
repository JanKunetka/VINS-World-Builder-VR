using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace WorldBuilderVR.Systems.XR
{
    /// <summary>
    /// Uses the <see cref="XRRayInteractor"/> to constantly scan for a position.
    /// </summary>
    public class XRRayScannerConstant : MonoBehaviour, IRaycaster
    {
        [SerializeField] private XRRayInteractor ray;

        private bool isScanning;
        private bool isHitting;
        private HitInfo hitInfo;
        
        private void Update()
        {
            DoScanning();
        }

        /// <summary>
        /// Starts to scan the position.
        /// <param name="activation">Will be called on ray hit.</param>
        /// </summary>
        public void BeginScanning()
        {
            isScanning = true;
        }

        /// <summary>
        /// Stops scanning for position.
        /// </summary>
        public void EndScanning()
        {
            isScanning = false;
            isHitting = false;
        }


        /// <summary>
        /// Scans the area.
        /// </summary>
        private void DoScanning()
        {
            if (!isScanning) return;
            
            isHitting = ray.TryGetCurrent3DRaycastHit(out RaycastHit hit);
            if (!isHitting) return;
            this.hitInfo = RaycastHitInfoPacker.Pack(hit);
        }

        public HitInfo HitInfo { get => hitInfo; }
        public bool IsHitting { get => isHitting; }
    }
}