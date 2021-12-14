using UnityEngine;

namespace WorldBuilderVR.Editors.Data.Core
{
    /// <summary>
    /// A Base for all assets.
    /// </summary>
    public abstract class AssetBase : IAssetBase
    {
        //TODO - For all assets upon creation, read author from Player Profile.
        protected string id;
        protected string title;
        protected Sprite icon;
        protected string author;

        /// <summary>
        /// Generates the ID for this asset.
        /// </summary>
        /// <returns>ID</returns>
        public void GenerateID(string assetIdentifier)
        {
            string authorPart = Mathf.Abs(author.GetHashCode()).ToString().Substring(0, 4);
            string randomPart = Random.Range(100, 999).ToString();
            id = assetIdentifier + authorPart + randomPart;
        }

        #region Update Values
        public void UpdateTitle(string newTitle)
        {
            this.title = newTitle;
        }

        public virtual void UpdateIcon(Sprite newIcon)
        {
            this.icon = newIcon;
        }
        #endregion
        
        public string ID { get => id; }
        public string Title { get => title; }
        public Sprite Icon { get => icon; }
        public string Author { get => author; }
    }
}