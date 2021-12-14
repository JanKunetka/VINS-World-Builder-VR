using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Data.Core;

namespace WorldBuilderVR.Ui.AssetSelection.Data
{
    public interface IAssetHolder
    {
        public int ID { get; }
        public AssetType Type { get; }
        public AssetBase Asset { get; }

        /// <summary>
        /// Should be called after creating an Asset Card. Constructs basic variables.
        /// </summary>
        /// <param name="type">The type of asset this is.</param>
        /// <param name="id">This asset's position in the list.</param>
        /// <param name="asset">The Asset itself.</param>
        public void Construct(AssetType type, int id, AssetBase asset);
    }
}