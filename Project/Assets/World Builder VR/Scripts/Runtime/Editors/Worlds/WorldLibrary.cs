using System.Collections.Generic;
using BoubakProductions.Safety;
using UnityEngine;
using WorldBuilderVR.Core;
using WorldBuilderVR.Systems.ExternalStorage;

namespace WorldBuilderVR.Editors.Worlds
{
    /// <summary>
    /// Contains the list of all worlds and deals with their saving/loading.
    /// </summary>
    public class WorldLibrary
    {
        private readonly WorldList worlds = new WorldList();

        #region Singleton Pattern
        private static WorldLibrary instance;
        private static readonly object padlock = new object();
        public static WorldLibrary Instance 
        { 
            get
            {
                // lock (padlock)
                {
                    if (instance == null)
                        instance = new WorldLibrary();
                    return instance;
                }
            }
        }
        #endregion

        private WorldLibrary()
        {
            WorldEditorOverseer.Instance.OnSaveChanges -= UpdateWorld;
            WorldEditorOverseer.Instance.OnSaveChanges += UpdateWorld;
            ReloadFromExternalStorage();
        }

        /// <summary>
        /// Repopulates the library with all worlds located on external storage.
        /// </summary>
        public void ReloadFromExternalStorage()
        {
            worlds.ReplaceAll(ExternalStorageOverseer.Instance.LoadAllWorlds());
        }

        /// <summary>
        /// Creates a new world, and adds it to the library.
        /// </summary>
        /// <param name="title">The title of the world.</param>
        /// <param name="icon">The icon of the world.</param>
        /// <param name="author">The author of the World.</param>
        public void CreateAndAddWorld(string title, Sprite icon, string author, PlacedObjectInfo playerLastPosition, string stageID, IList<string> modelPackIDs)
        {
            WorldAsset newWorld = new WorldAsset(title, icon, author, playerLastPosition, stageID, modelPackIDs);
            worlds.Add(newWorld);
        }

        /// <summary>
        /// Updates the world on a specific position in the library
        /// </summary>
        /// <param name="world">The new data for the world.</param>
        /// <param name="index">Position to override.</param>
        /// <param name="originalTitle">world's Title before updating.</param>
        /// <param name="originalAuthor">world's Author before updating.</param>
        public void UpdateWorld(WorldAsset world, int index, string originalTitle, string originalAuthor)
        {
            if (worlds.TryFinding(originalTitle, originalAuthor) != null)
                ExternalStorageOverseer.Instance.DeleteWorld(originalTitle);
            worlds.Update(index, world);
        }
        
        /// <summary>
        /// Remove world from the library.
        /// </summary>
        /// <param name="worldIndex">world ID in the list.</param>
        public void DeleteWorld(int worldIndex)
        {
            worlds.Remove(worldIndex);
        }

        /// <summary>
        /// Prepare one of the worlds in the library for editing.
        /// </summary>
        public void ActivateWorldEditor(int worldIndex)
        {
            SafetyNet.EnsureListIsNotEmptyOrNull(worlds, "World Library");
            SafetyNet.EnsureIntIsInRange(worldIndex, 0, worlds.Count, "World Library Pos Index");
            WorldEditorOverseer.Instance.AssignAsset(worlds[worldIndex], worldIndex);
        }

        /// <summary>
        /// Returns a stage with a given ID.
        /// </summary>
        /// <param name="id">The id of stage to get.</param>
        /// <returns></returns>
        public WorldAsset GetByID(string id)
        {
            return worlds[worlds.FindIndexFirst(id)];
        }
        
        /// <summary>
        /// Amount of worlds, stored in the library.
        /// </summary>
        public int WorldCount => worlds.Count;

        /// <summary>
        /// Returns a copy of the list of worlds stored here.
        /// </summary>
        /// <returns>A copy of world Library.</returns>
        public WorldList GetWorldsCopy => new WorldList(worlds);
    }
}