using System;
using UnityEngine;
using UnityEngine.UI;
using WorldBuilderVR.Editors.Models;

namespace WorldBuilderVR.Ui.Catalog
{
    /// <summary>
    /// Holds information about a specific asset.
    /// </summary>
    public class CatalogItem : MonoBehaviour
    {
        public static event Action<int> OnAssetActivate; 

        [SerializeField] private Image iconImage;
        [SerializeField] private Button activationButton;
        
        private int index;
        private ModelAsset asset;

        private void Awake()
        {
            activationButton.onClick.AddListener(WhenActivated);
        }

        /// <summary>
        /// Constructs the Catalog Item with relevant data.
        /// </summary>
        /// <param name="index">The index of the asset on the library list.</param>
        /// <param name="asset">The asset itself.</param>
        public void Construct(int index, ModelAsset asset)
        {
            this.index = index;
            this.asset = asset;
            this.iconImage.sprite = asset.Icon;
        }

        private void WhenActivated() => OnAssetActivate?.Invoke(index);

        public Button ActivationButton { get => activationButton; }
        public int Index { get => index; }
        public ModelAsset Asset { get => asset; }
    }
}