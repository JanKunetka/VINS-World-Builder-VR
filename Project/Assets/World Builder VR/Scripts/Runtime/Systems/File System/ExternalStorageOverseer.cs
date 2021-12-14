using System.Collections.Generic;
using System.IO;
using UnityEngine;
using WorldBuilderVR.Editors.Worlds;

namespace WorldBuilderVR.Systems.ExternalStorage
{
    /// <summary>
    /// Overseers files stored on external storage.
    /// </summary>
    public class ExternalStorageOverseer
    {
        private SaveableData worldData;

        #region Singleton Pattern
        private static ExternalStorageOverseer instance;
        private static readonly object padlock = new object();
        public static ExternalStorageOverseer Instance 
        { 
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new ExternalStorageOverseer();
                    return instance;
                }
            }
        }
        #endregion

        private ExternalStorageOverseer()
        {
            worldData = new SaveableData("Player Worlds", "vrworld");

            //Create directories if they don't exist.
            if (!Directory.Exists(worldData.Path))
                Directory.CreateDirectory(worldData.Path);
        }

        /// <summary>
        /// Wrapper for saving a world to external storage.
        /// </summary>
        /// <param name="world">The world to save.</param>
        public void Save(WorldAsset world)
        {
            string savePath = Path.Combine(worldData.Path, $"{world.Title}.{worldData.Extension}");
            FileSystem.SaveWorld(savePath, world);
        }
  

        /// <summary>
        /// Wrapper for loading all worlds from external storage.
        /// </summary>
        /// <returns></returns>
        public IList<WorldAsset> LoadAllWorlds()
        {
            return FileSystem.LoadAllWorlds(worldData.Path);
        }

        /// <summary>
        /// Wrapper for removing a world from external storage.
        /// <param name="world">The world to remove.</param>
        /// </summary>
        public void DeleteWorld(WorldAsset world)
        {
            DeleteWorld(world.Title);
        }
        /// <summary>
        /// Wrapper for removing a world from external storage.
        /// </summary>
        /// <param name="worldTitle">The name of the world to remove.</param>
        public void DeleteWorld(string worldTitle)
        {
            string removePath = Path.Combine(worldData.Path, $"{worldTitle}.{worldData.Extension}");
            FileSystem.DeleteFile(removePath);
        }

        public string worldPath {get => worldData.Path;}
        public string worldExtension {get => worldData.Extension;}
    }
}