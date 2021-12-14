using BoubakProductions.Core;
using UnityEngine;

namespace WorldBuilderVR.Editors.Models
{
    /// <summary>
    /// Fills the Model Pack Library with packs, collected from Scriptable Objects.
    /// </summary>
    public class ModelPackLibraryLoader : MonoSingleton<ModelPackLibraryLoader>
    {
        [SerializeField] private ModelPackAsset[] modelPacks;

        protected override void Awake()
        {
            base.Awake();
            ModelPackLibrary.Instance.Reload(modelPacks);
        }
    }
}