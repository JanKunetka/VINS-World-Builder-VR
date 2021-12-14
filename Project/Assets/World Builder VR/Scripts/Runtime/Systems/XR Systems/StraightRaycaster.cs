using System;
using UnityEngine;

namespace WorldBuilderVR.Systems.XR
{
    /// <summary>
    /// Shoots a Raycast in this object's forward Direction.
    /// </summary>
    public class StraightRaycaster : MonoBehaviour, IRaycaster
    {
        public event Action<bool> OnChangeBlockStatus;
        
        [SerializeField] private float maxDistance;
        [SerializeField] private LayerMask detectMask;
        [SerializeField] private LayerMask blockMask;
        [SerializeField] private bool showGizmos;

        private Transform ttransform;
        private HitInfo hitInfo;
        private bool isScanning;

        private void Awake()
        {
            ttransform = transform;
        }

        private void Update()
        {
            DoScanning();
        }

        public void BeginScanning()
        {
            isScanning = true;
        }

        public void EndScanning()
        {
            isScanning = false;
        }

        private void DoScanning()
        {
            if (!isScanning) return;
            if (Physics.Raycast(ttransform.position, ttransform.forward, out RaycastHit hit, maxDistance, detectMask))
            {
                hitInfo.transform = hit.transform;
                hitInfo.point = hit.point;
                hitInfo.normal = hit.normal;
                ChangeBlockStatus(IsInLayerMask(hit.transform.gameObject, blockMask));
            }
            else
            {
                hitInfo.transform = null;
                hitInfo.point = Vector3.zero;
                hitInfo.normal = Vector3.zero;
            }
        }

        private void ChangeBlockStatus(bool value)
        {
            if (hitInfo.isBlocked == value) return;
            hitInfo.isBlocked = value;
            OnChangeBlockStatus?.Invoke(value);
        }

        private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
        {
            return ((layerMask.value & (1 << obj.layer)) > 0);
        }
        
        private void OnDrawGizmos()
        {
            if (!showGizmos) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
        }

        public HitInfo HitInfo { get => hitInfo; }
    }
}