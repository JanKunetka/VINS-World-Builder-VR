using System;
using BoubakProductions.Safety;
using WorldBuilderVR.Core;
using WorldBuilderVR.Ui.AssetSelection;

namespace WorldBuilderVR.Systems.GASExtension
{
    /// <summary>
    /// Contains GAS Method specific to World Builder VR
    /// </summary>
    public static class GASWorldVR
    {
        public static AssetSelectionOverseerMono assetSelection;
        
        public static void ReopenSelectionMenu(AssetType type)
        {
            SafetyNet.EnsureIsNotNull(assetSelection, "GAS Asset Selection");
            switch (type)
            {
                case AssetType.World:
                    assetSelection.ReopenForWorlds();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"'{type}' is not a supported Asset Type.");
            }
        }
    }
}