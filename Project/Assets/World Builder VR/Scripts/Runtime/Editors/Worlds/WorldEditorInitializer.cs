using System;
using UnityEngine;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Models;
using WorldBuilderVR.Editors.Stages;
using WorldBuilderVR.Systems.SceneTransfer;

namespace WorldBuilderVR.Editors.Worlds
{
    /// <summary>
    /// Initializes the World Editor.
    /// </summary>
    public class WorldEditorInitializer : MonoBehaviour
    {
        public event Action<ModelPackAsset> OnModelPackLoad;
        
        [SerializeField] private Transform stageParent;
        [SerializeField] private Transform playerTransform;

        private void Awake()
        {
            SceneTransferService.GetInstance().PickUp(out WorldAsset world, out int worldIndex, out StageAsset stage, out ModelPackAsset modelPack);
            
            SpawnWorld(stage);
            PreparePlayer(world.PlayerPosInfo);
            WorldEditorOverseer.Instance.AssignAsset(world, worldIndex);
            
            OnModelPackLoad?.Invoke(modelPack);
        }

        /// <summary>
        /// Spawn the stage into position.
        /// </summary>
        /// <param name="stage">The Stage to spawn.</param>
        private void SpawnWorld(StageAsset stage)
        {
            Instantiate(stage.Stage, stageParent);
        }

        /// <summary>
        /// Places the Player into the right position.
        /// </summary>
        /// <param name="placeInfo">The Transform information about where to spawn the player.</param>
        private void PreparePlayer(PlacedObjectInfo placeInfo)
        {
            TransformPacker.Unpack(placeInfo, playerTransform);
        }
        
    }
}