using BoubakProductions.Core;
using UnityEngine;

namespace WorldBuilderVR.Core
{
    /// <summary>
    /// Packs and unpacks object's positional information into a struct. (<see cref="PlacedObjectInfo"/>)
    /// </summary>
    public static class TransformPacker
    {
        /// <summary>
        /// Packs transform information into a pack.
        /// </summary>
        /// <param name="objectTransform">The transform, whose information will be packed.</param>
        /// <returns>The pack of important information.</returns>
        public static PlacedObjectInfo Pack(Transform objectTransform)
        {
            PlacedObjectInfo objectInfo;
            objectInfo.position = objectTransform.position;
            objectInfo.rotation = objectTransform.rotation;
            objectInfo.scale = objectTransform.localScale;
            return objectInfo;
        }

        /// <summary>
        /// Unpacks information from a pack and assigns it to a transform.
        /// </summary>
        /// <param name="objectInfo">The information to process.</param>
        /// <param name="objectTransform">The transform to receive the information.</param>
        public static void Unpack(PlacedObjectInfo objectInfo, Transform objectTransform)
        {
            objectTransform.position = objectInfo.position;
            objectTransform.rotation = objectInfo.rotation;
            objectTransform.localScale = objectInfo.scale;
        }
        
    }
}