using System.Collections.Generic;
using UnityEngine;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Core.Defaults;
using WorldBuilderVR.Editors.Data.Core;

namespace WorldBuilderVR.Editors.Worlds
{
    /// <summary>
    /// An Asset holding information about a player created world.
    /// </summary>
    public class WorldAsset : AssetBase
    {
        private PlacedObjectInfo playerPosInfo;
        private string stageID;
        private IList<string> modelPackIDs;
        private IDictionary<string, IList<PlacedObjectInfo>> placedAssets;

        #region Constructors

        public WorldAsset()
        {
            title = EditorDefaults.WorldTitle;
            icon = EditorDefaults.WorldIcon;
            author = EditorDefaults.Author;
            
            modelPackIDs = new List<string>();
            placedAssets = new Dictionary<string, IList<PlacedObjectInfo>>();
            
            GenerateID(EditorAssetIDs.WorldIdentifier);
        }

        public WorldAsset(WorldAsset asset)
        {
            title = asset.Title;
            icon = asset.Icon;
            author = asset.Author;

            playerPosInfo = asset.PlayerPosInfo;
            stageID = asset.StageID;
            modelPackIDs = new List<string>(asset.ModelPackIDs);
            placedAssets = new Dictionary<string, IList<PlacedObjectInfo>>(asset.PlacedAssets);
            
            GenerateID(EditorAssetIDs.WorldIdentifier);
        }

        public WorldAsset(string title, Sprite icon, string author, PlacedObjectInfo playerPosInfo, string stageID, IList<string> modelPackIDs)
        {
            this.title = title;
            this.icon = icon;
            this.author = author;
            
            this.playerPosInfo = playerPosInfo;
            this.stageID = stageID;
            this.modelPackIDs = new List<string>(modelPackIDs);
            this.placedAssets = new Dictionary<string, IList<PlacedObjectInfo>>();
            
            GenerateID(EditorAssetIDs.WorldIdentifier);
        }
        
        public WorldAsset(string id, string title, Sprite icon, string author, PlacedObjectInfo playerPosInfo, string stageID, IList<string> modelPackIDs, IDictionary<string, IList<PlacedObjectInfo>> placedAssets)
        {
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            
            this.playerPosInfo = playerPosInfo;
            this.stageID = stageID;
            this.modelPackIDs = new List<string>(modelPackIDs);
            this.placedAssets = new Dictionary<string, IList<PlacedObjectInfo>>(placedAssets);
        }
        #endregion

        #region Update Values
        public void UpdatePlacedAssets(IDictionary<string, IList<PlacedObjectInfo>> newPlacedObjects)
        {
            this.placedAssets = new Dictionary<string, IList<PlacedObjectInfo>>(newPlacedObjects);
        }
        #endregion
        
        public PlacedObjectInfo PlayerPosInfo { get => playerPosInfo; }
        public string StageID { get => stageID; }
        public IList<string> ModelPackIDs { get => modelPackIDs; }
        public IDictionary<string, IList<PlacedObjectInfo>> PlacedAssets { get => placedAssets; }
    }
}