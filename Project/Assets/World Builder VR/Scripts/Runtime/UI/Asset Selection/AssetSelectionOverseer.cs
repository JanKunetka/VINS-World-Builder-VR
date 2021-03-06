using BoubakProductions.Safety;
using BoubakProductions.Systems.ObjectSwitching;
using System;
using System.Collections.Generic;
using BoubakProductions.Core;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Data.Core;
using WorldBuilderVR.Ui.AssetSelection.Data;
using Object = UnityEngine.Object;

namespace WorldBuilderVR.Ui.AssetSelection
{
    /// <summary>
    /// Is responsible for controlling the Asset Selection Menu, and switching out content as needed in it.
    /// </summary>
    public class AssetSelectionOverseer
    {
        public event Action<IAssetHolder> OnSpawnCard;
        public event Action OnFinishedFilling;
        
        private readonly IList<IAssetHolder> assets;
        
        private ObjectSwitcherMono layoutSwitcher;
        private AssetType lastTypeOpen;
        
        #region Singleton Pattern
        private static AssetSelectionOverseer instance;
        private static readonly object padlock = new object();

        public static AssetSelectionOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new AssetSelectionOverseer();
                    return instance;
                }
            }
        }

        #endregion

        private AssetSelectionOverseer()
        {
            assets = new List<IAssetHolder>();
            lastTypeOpen = AssetType.None;
        }

        /// <summary>
        /// Sets up the selection menu.
        /// </summary>
        public void Setup(AssetType type, SelectionMenuLayout layout, SelectionMenuAsset asset, IList<AssetBase> assetList, ObjectSwitcherMono layoutSwitcher)
        {
            this.layoutSwitcher = layoutSwitcher;
            this.layoutSwitcher.Switcher.DeselectAllExcept(layout.Menu);

            //Do not refill, if menu is the same.
            if (type == lastTypeOpen && type != AssetType.None)
            {
                if (assetList.Count != assets.Count)
                {
                    //Spawn newly created cards.
                    int newCards = assetList.Count - assets.Count;

                    //If a pack was removed, reload the list.
                    if (newCards > 0)
                    {
                        for (int i = 0; i < newCards; i++)
                        {
                            SpawnCard(assetList.Count + (i-1), type, layout, asset, assetList);
                        }
                        return;
                    }
                }
            }

            RefillMenu(type, layout, asset, assetList);
            lastTypeOpen = type;
            OnFinishedFilling?.Invoke();
        }
        
        /// <summary>
        /// Fills the selection menu canvas with asset holder objects.
        /// <param name="type">The type of asset this card will be.</param>
        /// <param name="layout">Which layout is used.</param>
        /// <param name="asset">What kind of data will be used.</param>
        /// <param name="assetList">The list to take the data from.</param>
        /// </summary>
        private void RefillMenu(AssetType type, SelectionMenuLayout layout, SelectionMenuAsset asset, IList<AssetBase> assetList)
        {
            SafetyNet.EnsureIsNotNull(asset.assetObject.GetComponent<IAssetHolder>(), "IAssetHolder in RefillMenu");
            
            //Clear assets from content, respawn add button
            assets.Clear();
            layout.Content.KillChildren();
            if (asset.addButton != null)
                Object.Instantiate(asset.addButton, layout.Content.transform);

            //Spawn new assets
            for (int i = 0; i < assetList.Count; i++)
            {
                SpawnCard(i, type, layout, asset, assetList);
            }
        }

        /// <summary>
        /// Spawns the clickable UI Card itself.
        /// </summary>
        /// <param name="id">Position on the list it spawns the card on.</param>
        /// <param name="type">The type of asset this card will be.</param>
        /// <param name="layout">Which layout is used.</param>
        /// <param name="asset">What kind of data will be used.</param>
        /// <param name="assetList">The list to take the data from.</param>
        private void SpawnCard(int id, AssetType type, SelectionMenuLayout layout, SelectionMenuAsset asset, IList<AssetBase> assetList)
        {
            IAssetHolder holder = Object.Instantiate(asset.assetObject, layout.Content.transform).GetComponent<IAssetHolder>();
            holder.Construct(type, id, assetList[id]);
            OnSpawnCard?.Invoke(holder);
            assets.Add(holder);
        }
        
        /// <summary>
        /// Get amount of assets showcased by the menu.
        /// </summary>
        public int AssetCount => assets.Count;
        
    }
}