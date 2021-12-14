using UnityEngine;

namespace WorldBuilderVR.Systems.XR
{
    /// <summary>
    /// Packs & Unpacks only the necessary information from a Raycast Hit. 
    /// </summary>
    public static class RaycastHitInfoPacker
    {
        /// <summary>
        /// Packs a Hit into a <see cref="HitInfo"/>.
        /// </summary>
        /// <param name="hit">The <see cref="RaycastHit"/> struct.</param>
        /// <returns>A <see cref="HitInfo"/> with all important information.</returns>
        public static HitInfo Pack(RaycastHit hit)
        {
            return Pack(hit, false);
        }
        /// <summary>
        /// Packs a Hit into a <see cref="HitInfo"/>.
        /// </summary>
        /// <param name="hit">The <see cref="RaycastHit"/> struct.</param>
        /// <param name="isBlocked">The value keeping track of if the raycast is blocked or not.</param>
        /// <returns>A <see cref="HitInfo"/> with all important information.</returns>
        public static HitInfo Pack(RaycastHit hit, bool isBlocked)
        {
            HitInfo hitInfo;
            hitInfo.normal = hit.normal;
            hitInfo.point = hit.point;
            hitInfo.transform = hit.transform;
            hitInfo.isBlocked = isBlocked;
            return hitInfo;
        }
    }
}