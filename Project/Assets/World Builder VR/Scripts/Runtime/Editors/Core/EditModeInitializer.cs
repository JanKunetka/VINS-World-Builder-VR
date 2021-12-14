using BoubakProductions.Core;
using UnityEngine;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Models;
using WorldBuilderVR.Editors.Worlds;
using WorldBuilderVR.Systems.WorldPlacer;
using WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette.TransformOverseer;
using WorldBuilderVR.Ui.Catalog;

namespace WorldBuilderVR.Editors.Core
{
    /// <summary>
    /// Works like a bridge between the game and <see cref="EditModeOverseer"/>.
    /// </summary>
    public class EditModeInitializer : MonoSingleton<EditModeInitializer>, IOverseerInitializer
    {
        [SerializeField] private WorldEditorInitializer worldEditorInitializer;
        [SerializeField] private WorldPlacerOverseerInitializer worldPlacerInitializer;
        [SerializeField] private SilhouetteTransformOverseerInitializer transformInitializer;
        [SerializeField] private CatalogOverseerMono catalogOverseer;        
        
        private EditModeOverseer editor;
        private ModelPackAsset modelPack;

        private void OnEnable()
        {
            worldEditorInitializer.OnModelPackLoad += BeginInitialization;
            
        }
        private void OnDisable()
        {
            worldEditorInitializer.OnModelPackLoad -= BeginInitialization;
        }

        private void BeginInitialization(ModelPackAsset modelPack)
        {
            this.modelPack = modelPack;
            Initialize();
        }

        public void Initialize()
        {
            editor = EditModeOverseer.Instance;
            
            CatalogItem.OnAssetActivate -= editor.SwitchModeToPlacement;
            CatalogItem.OnAssetActivate += editor.SwitchModeToPlacement;
            
            worldPlacerInitializer.Initialize();
            transformInitializer.Initialize();
            editor.AssignAsset(modelPack);
            
            catalogOverseer.Fill(editor.CurrentPack);
        }
    }
}