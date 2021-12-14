using BoubakProductions.Safety;
using System;
using System.Collections.Generic;
using BoubakProductions.Core;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Models;

namespace WorldBuilderVR.Systems.PlacedObjectRegistry
{
    /// <summary>
    /// Spawns objects and contains a log of their transform information.
    /// </summary>
    public class ObjectRegistry
    {
        private ModelLoader loader;
        
        private IDictionary<string, IList<PlacedObjectInfo>> registry;
        private IList<ModelAsset> models;

        public ObjectRegistry(IList<ModelAsset> models)
        {
            loader = new ModelLoader();
            registry = new Dictionary<string, IList<PlacedObjectInfo>>();
            this.models = new List<ModelAsset>(models);
        }

        /// <summary>
        /// Preloads the registry with an already existing one.
        /// </summary>
        /// <param name="initialRegistry"></param>
        public void Preload(IDictionary<string, IList<PlacedObjectInfo>> initialRegistry)
        {
            registry = new Dictionary<string, IList<PlacedObjectInfo>>(initialRegistry);
            loader.LoadAll(models, registry);
        }
        
        /// <summary>
        /// Adds a new object to the registry.
        /// </summary>
        /// <param name="objectID">The ID of the object.</param>
        /// <param name="objectInfo">The positional info of the object.</param>
        public void Add(string objectID, PlacedObjectInfo objectInfo)
        {
            SafetyNet.EnsureStringMinLimit(objectID, 0, nameof(objectID));

            if (!registry.ContainsKey(objectID))
                registry.Add(objectID, new List<PlacedObjectInfo>());
            registry[objectID].Add(objectInfo);
            
            loader.Load(models, objectID, objectInfo);
        }

        /// <summary>
        /// Removes an object from the registry.
        /// </summary>
        /// <param name="objectID">The Object ID.</param>
        /// <param name="objectInfo">The object's positional info.</param>
        public void Remove(string objectID, PlacedObjectInfo objectInfo)
        {
            SafetyNet.EnsureStringMinLimit(objectID, 0, nameof(objectID));
            if (!registry.ContainsKey(objectID)) return;
            if (registry[objectID] == null) return;
            
            registry[objectID].RemoveAt(FindObjectInfo(registry[objectID], objectInfo));
        }

        /// <summary>
        /// Finds a KeyValuePair in the registry.
        /// </summary>
        /// <param name="objectInfos">The list of placed infos to search.</param>
        /// <param name="info">The positional info to search for.</param>
        /// <returns>A found KeyValuePair.</returns>
        /// <exception cref="InvalidOperationException">Is thrown when no KeyValuePair was found.</exception>
        private int FindObjectInfo(IList<PlacedObjectInfo> objectInfos, PlacedObjectInfo info)
        {
            for (int i = 0; i < objectInfos.Count; i++)
            {
                float distance = (objectInfos[i].position - info.position).sqrMagnitude;
                if (distance > 0.05f) continue;
                return i;
            }

            throw new InvalidOperationException($"No PlacedObjectInfo of pos '{info.position}' rot '{info.rotation}' scale '{info.scale}' was found.");
        }

        /// <summary>
        /// Return the copy of the Registry.
        /// </summary>
        public IDictionary<string, IList<PlacedObjectInfo>> GetCopy => new Dictionary<string, IList<PlacedObjectInfo>>(registry);
    }
}