using UnityEngine;

namespace WorldBuilderVR.Systems.ExternalStorage
{
    /// <summary>
    /// Contains folder path information for saveable assets for ease of use.
    /// </summary>
    public class SaveableData
    {
        private string folderTitle;
        private string extension;
        private string path;

        public SaveableData(string folderTitle, string extension)
        {
            this.folderTitle = folderTitle;
            this.extension = extension;
            this.path = System.IO.Path.Combine(Application.persistentDataPath, folderTitle);
        }
        
        public string FolderName { get => folderTitle; }
        public string Extension { get => extension; }
        public string Path { get => path; }
    }
}