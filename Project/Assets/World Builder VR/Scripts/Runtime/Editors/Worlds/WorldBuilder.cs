using System.Collections.Generic;
using UnityEngine;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Core.Defaults;
using WorldBuilderVR.Editors.Models;
using WorldBuilderVR.Editors.Stages;

namespace WorldBuilderVR.Editors.Worlds
{
    /// <summary>
    /// A class meant to quickly build a World Pack.
    /// </summary>
    public class WorldBuilder
    {
        /// <summary>
        /// Build a World Pack and save it to the library.
        /// </summary>
        public void Build()
        {
            string title = $"World #{Random.Range(0, 100)}{Random.Range(0, 1000)}";
            Sprite icon = EditorDefaults.WorldIcon;
            string author = "Me";
            
            PlacedObjectInfo playerPos;
            playerPos.position = StageLibrary.Instance.GetCopy[0].StartingPosition;
            playerPos.rotation = Quaternion.Euler(StageLibrary.Instance.GetCopy[0].StartingRotation);
            playerPos.scale = Vector3.one;
            
            string stageID = StageLibrary.Instance.GetCopy[0].ID;
            IList<string> modelPackIDs = ModelPackLibrary.Instance.GetCopy.ConvertToIDs();
            
            WorldLibrary.Instance.CreateAndAddWorld(title, icon, author, playerPos, stageID, modelPackIDs);
        }
    }
}