using BoubakProductions.Core;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Models;
using WorldBuilderVR.Editors.Worlds;
using WorldBuilderVR.Systems.WorldPlacer;
using WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette.TransformOverseer;
using WorldBuilderVR.Ui.Catalog;
using WorldBuilderVR.User;
using UnityEngine;

namespace WorldBuilderVR.Editors.Core
{
    /// <summary>
    /// Initializes the Edit Mode Scene and assigns properties to the <see cref="EditModeOverseer"/>.
    /// </summary>
    public class EditModeInitializer : MonoSingleton<EditModeInitializer>, IOverseerInitializer
    {
        [SerializeField] private WorldEditorInitializer worldEditorInitializer;
        [SerializeField] private WorldPlacerOverseerInitializer worldPlacerInitializer;
        [SerializeField] private SilhouetteTransformOverseerInitializer transformInitializer;
        [SerializeField] private CatalogOverseerMono catalogOverseer;
        [SerializeField] private MenuVisibilityController catalogController;
        
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
            
            catalogController.SwitchMenuStatus();
            
            CatalogItem.OnAssetActivate -= editor.SwitchModeToPlacement;
            CatalogItem.OnAssetActivate += editor.SwitchModeToPlacement;
            
            worldPlacerInitializer.Initialize();
            transformInitializer.Initialize();
            editor.AssignAsset(modelPack);
            
            catalogOverseer.Fill(editor.CurrentPack);
        }
    }
}