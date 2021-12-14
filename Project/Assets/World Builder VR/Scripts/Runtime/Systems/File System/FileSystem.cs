using BoubakProductions.Safety;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WorldBuilderVR.Editors.Worlds;
using WorldBuilderVR.Systems.ExternalStorage.Serialization;

namespace WorldBuilderVR.Systems.ExternalStorage
{
    /// <summary>
    /// Handles Saving, Loading and Removing worlds from external storage.
    /// </summary>
    public static class FileSystem
    {
        /// <summary>
        /// Save an asset to external storage.
        /// </summary>
        /// <param name="path">Destination of the save.</param>
        /// <param name="world">The world to save.</param>
        public static void SaveWorld(string path, WorldAsset world)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            SerializedWorldAsset formattedAsset = new SerializedWorldAsset(world);

            formatter.Serialize(stream, formattedAsset);
            stream.Close();
        }

        /// <summary>
        /// Loads all assets under a path.
        /// </summary>
        /// <param name="path">Destination of the save.</param>
        public static IList<WorldAsset> LoadAllWorlds(string path)
        {
            SafetyNetIO.EnsureDirectoryExists(path);

            IList<WorldAsset> assets = new List<WorldAsset>();

            BinaryFormatter formatter = new BinaryFormatter();
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files)
            {
                string filePath = Path.Combine(path, file.Name);
                FileStream stream = new FileStream(filePath, FileMode.Open);

                SerializedWorldAsset asset = (SerializedWorldAsset)formatter.Deserialize(stream);
                assets.Add(asset.Deserialize());

                stream.Close();
            } 

            return assets;
        }

        /// <summary>
        /// Removes a file under a specific path.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public static void DeleteFile(string path)
        {
            SafetyNetIO.EnsureFileExists(path);
            File.Delete(path);
        }
        
        /// <summary>
        /// Removes a directory under a specific path.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        public static void DeleteDirectory(string path)
        {
            SafetyNetIO.EnsureDirectoryExists(path);
            Directory.Delete(path);
        }
    }
}