using UnityEngine;
using WorldBuilderVR.Editors.Core.Defaults;
using WorldBuilderVR.Editors.Data.Core;

namespace WorldBuilderVR.Editors.Models
{
    /// <summary>
    /// An Asset holding a reference to a model.
    /// </summary>
    [CreateAssetMenu(fileName = "New Model Asset", menuName = "World Builder VR/Model Asset", order = 1)]
    public class ModelAsset : AssetBaseSO
    {
        [SerializeField, TextArea] private string description;
        [SerializeField] private CategoryType category;
        [SerializeField] private GameObject model;

        private void Awake()
        {
            GenerateID(EditorAssetIDs.ModelIdentifier);
        }

        public string Description { get => description;}
        public CategoryType Category { get => category;}
        public GameObject Model { get => model;}
    }
}