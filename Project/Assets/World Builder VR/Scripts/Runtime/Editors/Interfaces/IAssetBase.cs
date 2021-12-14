using UnityEngine;

namespace WorldBuilderVR.Editors.Data.Core
{
    public interface IAssetBase
    {
        string ID { get; }
        string Title { get; }
        Sprite Icon { get; }
        string Author { get; }

        /// <summary>
        /// Generates the ID for this asset.
        /// </summary>
        /// <returns>ID</returns>
        void GenerateID(string assetIdentifier);
    }
}