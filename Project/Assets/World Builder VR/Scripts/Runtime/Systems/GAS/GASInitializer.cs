using BoubakProductions.Core;
using UnityEngine;
using WorldBuilderVR.Ui.AssetSelection;

namespace WorldBuilderVR.Systems.GASExtension
{
    /// <summary>
    /// Allows GAS to access MonoBehaviours.
    /// </summary>
    public class GASInitializer : MonoSingleton<GASInitializer>
    {
        [SerializeField] private AssetSelectionOverseerMono assetSelection;

        protected override void Awake()
        {
            GASWorldVR.assetSelection = assetSelection;
        }

        public AssetSelectionOverseerMono AssetSelection { get => assetSelection; }
    }
}