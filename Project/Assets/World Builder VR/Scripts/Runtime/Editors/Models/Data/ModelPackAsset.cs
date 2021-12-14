using System;
using UnityEngine;
using WorldBuilderVR.Editors.Core.Defaults;
using WorldBuilderVR.Editors.Data.Core;

namespace WorldBuilderVR.Editors.Models
{
    /// <summary>
    /// A Model Pack containing data about worlds and models.
    /// </summary>
    [CreateAssetMenu(fileName = "New Model Pack", menuName = "World Builder VR/Model Pack", order = 0)]
    public class ModelPackAsset : AssetBaseSO
    {
        [SerializeField, TextArea] private string description;
        [Space]
        [SerializeField] private ModelAsset[] models;

        private void Awake()
        {
            GenerateID(EditorAssetIDs.ModelPackIdentifier);
        }

        public void UpdateModels(ModelAsset[] newModels)
        {
            models = newModels;
        }
        
        public string Description { get => description; }
        public ModelAsset[] Models {get => models;}
    }
}