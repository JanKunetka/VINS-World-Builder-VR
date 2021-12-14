using System.Collections.Generic;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Models;
using WorldBuilderVR.Systems.ObjectSpawning;

namespace WorldBuilderVR.Systems.PlacedObjectRegistry
{
    /// <summary>
    /// Loads models into the world.
    /// </summary>
    public class ModelLoader
    {
        private readonly IObjectSpawner spawner;
        
        private ModelAsset lastModel;
        private IList<ModelAsset> usedModels;
        
        public ModelLoader()
        {
            usedModels = new List<ModelAsset>();
            this.spawner = new ObjectSpawnerBasic();
        }
        
        /// <summary>
        /// Spawns all models in a dictionary.
        /// </summary>
        /// <param name="models">A list of models to search in.</param>
        /// <param name="data">A dictionary of model IDs and their transform data.</param>
        public void LoadAll(IList<ModelAsset> models, IDictionary<string, IList<PlacedObjectInfo>> data)
        {
            foreach (KeyValuePair<string, IList<PlacedObjectInfo>> pair in data)
            {
                foreach (PlacedObjectInfo pos in pair.Value)
                {
                    Load(models, pair.Key, pos);
                }
            }
        }

        /// <summary>
        /// Spawns a model into existence, if it's ID is stored in the list of models.
        /// </summary>
        /// <param name="models">a list of models to search in.</param>
        /// <param name="id">An ID of a model.</param>
        /// <param name="objectInfo">Transform info of the object.</param>
        public void Load(IList<ModelAsset> models, string id, PlacedObjectInfo objectInfo)
        {
            //When the asset is the same as the previous one.
            if (lastModel != null && id == lastModel.ID)
            {
                spawner.Spawn(lastModel.Model, objectInfo);
                return;
            }

            //Search through models, that were already used.
            foreach (ModelAsset usedModel in usedModels)
            {
                if (usedModel.ID != id) continue;
                    
                spawner.Spawn(usedModel.Model, objectInfo);
                lastModel = usedModel;
                return;
            }
                
            //Search through he entire collection.
            foreach (ModelAsset model in models)
            {
                if (id != model.ID) continue;
                    
                spawner.Spawn(model.Model, objectInfo);
                lastModel = model;
                return;
            }
                
            //Fail, because model was not found.
                
            // spawner.Spawn(model.Model, pair.Value);
        }
        
    }
}