using BoubakProductions.Core;
using UnityEngine;

namespace WorldBuilderVR.Editors.Stages
{
    /// <summary>
    /// Fills the Stage Library with stages, collected from Scriptable Objects.
    /// </summary>
    public class StageLibraryLoader : MonoSingleton<StageLibraryLoader>
    {
        [SerializeField] private StageAsset[] stages;

        protected override void Awake()
        {
            base.Awake();
            StageLibrary.Instance.Reload(stages);
        }
    }
}