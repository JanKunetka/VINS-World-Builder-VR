using UnityEngine;

namespace WorldBuilderVR.Editors.Data.Core
{
    /// <summary>
    /// A base for all assets that are of type Scriptable Object.
    /// </summary>
    public abstract class AssetBaseSO : ScriptableObject, IAssetBase
    {
        [SerializeField] protected string title;
        [SerializeField] protected Sprite icon;
        [SerializeField] protected string author;
        protected string id;
        
        public void GenerateID(string assetIdentifier)
        {
            string titlePart = (title != null) ? Mathf.Abs(title.GetHashCode()).ToString() : Random.Range(1000000, 99999999).ToString();
            string titlePart1 = titlePart.Substring(0, 4);
            string titlePart2 = titlePart.Substring(titlePart.Length-4, 4);
            
            string authorPart = (author != null) ? Mathf.Abs(author.GetHashCode()).ToString() : Random.Range(1000000, 99999999).ToString();
            id = assetIdentifier + titlePart1 + authorPart + titlePart2;
        }
        
        public string ID { get => id; }
        public string Title { get => title; }
        public Sprite Icon { get => icon; }
        public string Author { get => author; }
    }
}