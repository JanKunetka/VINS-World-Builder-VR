using System;
using BoubakProductions.Safety;
using UnityEngine;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Core;
using WorldBuilderVR.Editors.Models;
using WorldBuilderVR.Systems.PlacedObjectRegistry;

namespace WorldBuilderVR.Editors.Worlds
{
    /// <summary>
    /// Overseers the World Editor.
    /// </summary>
    public class WorldEditorOverseer : IEditorOverseer
    {
        public event Action<WorldAsset> OnAssignAsset;
        public event Action<WorldAsset, int, string, string> OnSaveChanges;

        private ObjectRegistry registry;
        
        private WorldAsset currentAsset;
        private int index;
        private string originalTitle;
        private string originalAuthor;
        
        #region Singleton Pattern
        private static WorldEditorOverseer instance;
        private static readonly object padlock = new object();
        public static WorldEditorOverseer Instance 
        { 
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new WorldEditorOverseer();
                    return instance;
                }
            }
        }
        #endregion

        private WorldEditorOverseer() { }

        /// <summary>
        /// Assign a World Asset to the editor.
        /// </summary>
        /// <param name="asset">The asset to edit.</param>
        /// <param name="index">The assets position in the library.</param>
        public void AssignAsset(WorldAsset asset, int index)
        {
            SafetyNet.EnsureIsNotNull(asset, "Assigned WorldAsset");
            SafetyNet.EnsureIntIsBiggerOrEqualTo(index, 0, "Assigned WorldAsset Index");
            this.currentAsset = new WorldAsset(asset);
            this.index = index;
            this.originalTitle = asset.Title;
            this.originalAuthor = asset.Author;
            
            registry = new ObjectRegistry(ModelPackCombiner.Gather(asset.ModelPackIDs));
            registry.Preload(asset.PlacedAssets);
            
            OnAssignAsset?.Invoke(currentAsset);
        }

        public void AddModel(ModelAsset model, PlacedObjectInfo modelInfo)
        {
            registry.Add(model.ID, modelInfo);
        }
        
        public void RemoveModel(ModelAsset model, PlacedObjectInfo modelInfo)
        {
            registry.Remove(model.ID, modelInfo);
        }
        
        public void CompleteEditing()
        {
            SaveChanges();
            currentAsset = null;
        }

        /// <summary>
        /// Call for saving changes.
        /// </summary>
        public void SaveChanges()
        {
            currentAsset.UpdatePlacedAssets(registry.GetCopy);
            OnSaveChanges?.Invoke(currentAsset, index, originalTitle, originalAuthor);
        }
        
        public WorldAsset CurrentAsset 
        {
            get
            {
                if (currentAsset == null)
                    throw new MissingReferenceException("The current asset is null. Did you forget to activate the editor?");
                return currentAsset;
            }
        }
    }
}