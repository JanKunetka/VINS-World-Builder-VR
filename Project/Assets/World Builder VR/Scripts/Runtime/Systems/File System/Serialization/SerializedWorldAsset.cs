using BoubakProductions.Systems.Serialization;
using System.Collections.Generic;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Worlds;

namespace WorldBuilderVR.Systems.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="WorldAsset"/> class.
    /// </summary>
    [System.Serializable]
    public class SerializedWorldAsset
    {
        private string id;
        private string title;
        private SerializedSprite icon;
        private string author;

        private SerializedPlacedObjectInfo playerPosInfo;
        private string stageID;
        private IList<string> modelPackIDs;
        private Dictionary<string, IList<SerializedPlacedObjectInfo>> placedAssets;

        public SerializedWorldAsset(WorldAsset asset)
        {
            this.id = asset.ID;
            this.title = asset.Title;
            this.icon = new SerializedSprite(asset.Icon);
            this.author = asset.Author;
            
            this.playerPosInfo = new SerializedPlacedObjectInfo(asset.PlayerPosInfo);
            this.stageID = asset.StageID;
            this.modelPackIDs = new List<string>(asset.ModelPackIDs);

            this.placedAssets = new Dictionary<string, IList<SerializedPlacedObjectInfo>>();
            foreach (KeyValuePair<string, IList<PlacedObjectInfo>> pair in asset.PlacedAssets)
            {
                this.placedAssets.Add(pair.Key, new List<SerializedPlacedObjectInfo>());
                foreach (PlacedObjectInfo info in pair.Value)
                {
                    this.placedAssets[pair.Key].Add(new SerializedPlacedObjectInfo(info));
                }
            }
        }

        /// <summary>
        /// Returns the World Asset in a Unity readable format.
        /// </summary>
        /// <returns></returns>
        public WorldAsset Deserialize()
        {
            IDictionary<string, IList<PlacedObjectInfo>> placedAssetsDeserialized = new Dictionary<string, IList<PlacedObjectInfo>>();
            foreach (KeyValuePair<string, IList<SerializedPlacedObjectInfo>> pair in placedAssets)
            {
                placedAssetsDeserialized.Add(pair.Key, new List<PlacedObjectInfo>());
                foreach (SerializedPlacedObjectInfo info in pair.Value)
                {
                    placedAssetsDeserialized[pair.Key].Add(info.Deserialize());
                }
            }

            return new WorldAsset(id,
                                  title,
                                  icon.Deserialize(),
                                  author,
                                  playerPosInfo.Deserialize(),
                                  stageID,
                                  modelPackIDs,
                                  placedAssetsDeserialized);
        }

    }
}