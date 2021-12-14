using WorldBuilderVR.Editors.Data.Core;

namespace WorldBuilderVR.Ui.Catalog
{
    /// <summary>
    /// Holds information about a given asset and an it's index of the position
    /// on a list where it was referenced from.
    /// </summary>
    public class ReferencedAsset
    {
        private AssetBaseSO asset;
        private int index;

        public ReferencedAsset(AssetBaseSO asset, int index)
        {
            this.asset = asset;
            this.index = index;
        }

        public AssetBaseSO Asset { get => asset;}
        public int Index {get => index;}
    }
}