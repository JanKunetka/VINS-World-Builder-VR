using UnityEngine;

namespace WorldBuilderVR.Ui.AssetSelection
{
    [CreateAssetMenu(fileName = "New Selection Menu Asset", menuName = "World Builder VR/Asset Selection Menu References", order = 100)]
    public class SelectionMenuAsset : ScriptableObject
    {
        public GameObject assetObject;
        public GameObject addButton;
    }
}