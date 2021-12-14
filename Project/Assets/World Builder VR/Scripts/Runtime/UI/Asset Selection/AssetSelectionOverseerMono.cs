using BoubakProductions.Systems.ObjectSwitching;
using System;
using System.Linq;
using UnityEngine;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Data.Core;
using WorldBuilderVR.Editors.Worlds;
using WorldBuilderVR.Ui.AssetSelection.Data;

namespace WorldBuilderVR.Ui.AssetSelection
{
    /// <summary>
    /// Prepares the Asset Selection system for choosing assets to edit.
    /// </summary>
    [RequireComponent(typeof(ObjectSwitcherMono))]
    public class AssetSelectionOverseerMono : MonoBehaviour, IAssetSelectionOverseer
    {
        [SerializeField] private LayoutInfo layouts;
        [SerializeField] private AssetSelectionMenuInfo selectionMenus;

        private AssetSelectionOverseer overseer;
        private WorldLibrary lib;
        private ObjectSwitcherMono layoutSwitcher;

        private Action<IAssetHolder> listeningMethod;
        
        private void Awake()
        {
            lib = WorldLibrary.Instance;
            layoutSwitcher = GetComponent<ObjectSwitcherMono>();
            
            overseer = AssetSelectionOverseer.Instance;
        }

        #region Open Selection Menu

        //TODO - Instead of the SelectionMenuAsset, make the default layout type load from the options save file.
        public void ReopenForWorlds()
        {
            overseer.Setup(AssetType.World,
                layouts.list,
                selectionMenus.world,
                lib.GetWorldsCopy.ToList<AssetBase>(),
                layoutSwitcher);
        }
        #endregion

        /// <summary>
        /// Start listening to every card that is spawned by the selection menu.
        /// Will no longer listen after the menu is filled.
        /// </summary>
        /// <param name="listeningMethod">The method that will work with the currently spawned card.</param>
        public void BeginListeningToSpawnedCards(Action<IAssetHolder> listeningMethod)
        {
            this.listeningMethod = listeningMethod;
            overseer.OnSpawnCard += this.listeningMethod;
            overseer.OnFinishedFilling += StopListeningToSpawnedCards;
            
        }

        /// <summary>
        /// Stop listening to every card that is spawned by the selection menu.
        /// </summary>
        private void StopListeningToSpawnedCards()
        {
            overseer.OnSpawnCard -= this.listeningMethod;
        }
        
        public GameObject ListMenu { get => layouts.list.Menu; }
        public GameObject GridMenu { get => layouts.grid.Menu; }
        
    }
}