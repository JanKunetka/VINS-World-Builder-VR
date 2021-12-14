using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Data.Core;

namespace WorldBuilderVR.Ui.AssetSelection.Data
{
    /// <summary>
    /// Handles setup and button interactions for the Asset Card object.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class AssetCardController : MonoBehaviour, IAssetHolder
    {
        [SerializeField] private UIInfo ui;

        private int id;
        private AssetType type;
        private AssetBase asset;
        private Button cardButton;

        private void Start()
        {
            cardButton = GetComponent<Button>();
        }

        public void Construct(AssetType type, int id, AssetBase asset)
        {
            this.type = type;
            this.id = id;
            this.asset = asset;
            ui.title.text = asset.Title;
            ui.author.text = asset.Author;
        }

        public int ID { get => id; }
        public AssetType Type { get => type; }
        public AssetBase Asset { get => asset; }

        [System.Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI title;
            public TextMeshProUGUI author;
        }
    }
}