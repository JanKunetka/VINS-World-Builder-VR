using BoubakProductions.Core;
using BoubakProductions.Safety;
using WorldBuilderVR.Core;
using System.Collections.Generic;
using WorldBuilderVR.Editors.Models;
using WorldBuilderVR.Editors.Stages;
using WorldBuilderVR.Editors.Worlds;

namespace WorldBuilderVR.Systems.SceneTransfer
{
    /// <summary>
    /// Stores data that will be transferred between different scenes.
    /// </summary>
    public class SceneTransferService : MonoSingleton<SceneTransferService>
    {
        private WorldLibrary worldLib;
        private StageLibrary stageLib;
        private ModelPackLibrary packsLib;
        private AssetCombiner combiner;
        
        private WorldAsset world;
        private int worldIndex;
        private StageAsset stage;
        private ModelPackAsset ultimateModelPack;

        protected override void Awake()
        {
            base.Awake();
            
            worldLib = WorldLibrary.Instance;
            stageLib = StageLibrary.Instance;;
            packsLib = ModelPackLibrary.Instance;
            combiner = new AssetCombiner();
            
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Prepares the correct data for transferring.
        /// </summary>
        /// <param name="worldIndex">The Index of the world to load.</param>
        public void LoadUp(int worldIndex)
        {
            WorldAsset world = worldLib.GetWorldsCopy[worldIndex];
            StageAsset stage = stageLib.GetByID(world.StageID);

            IList<ModelPackAsset> modelPacks = packsLib.GetCopy.GrabFromList(world.ModelPackIDs);
            ModelPackAsset modelPack = combiner.Combine(modelPacks);
            
            AssignLoadedData(world, worldIndex, stage, modelPack);
        }
        
        /// <summary>
        /// Allows to take data from the pickup storage.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="stage"></param>
        /// <param name="modelPack"></param>
        public void PickUp(out WorldAsset world, out int worldIndex, out StageAsset stage, out ModelPackAsset modelPack)
        {
            world = this.world;
            worldIndex = this.worldIndex;
            stage = this.stage;
            modelPack = this.ultimateModelPack;
        }
        
        /// <summary>
        /// Loads the Transfer storage with data.
        /// </summary>
        /// <param name="world"></param>
        /// <param name="stage"></param>
        /// <param name="modelPack"></param>
        private void AssignLoadedData(WorldAsset world, int worldIndex, StageAsset stage, ModelPackAsset modelPack)
        {
            SafetyNet.EnsureIsNotNull(world, "World Asset");
            SafetyNet.EnsureIsNotNull(stage, "Stage Asset");
            SafetyNet.EnsureIsNotNull(modelPack, "Model Pack Assets");
            this.world = world;
            this.worldIndex = worldIndex;
            this.stage = stage;
            this.ultimateModelPack = modelPack;
        }
    }
}