using WorldBuilderVR.Editors.Models;
using WorldBuilderVR.Editors.Worlds;
using WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette;

namespace WorldBuilderVR.Systems.WorldPlacer.ToolBox
{
    /// <summary>
    /// The tool used for placing objects.
    /// </summary>
    public class PlacerTool : ITool
    {
        private ModelAsset activeModel;
        private ModelSilhouette silhouette;
        
        /// <summary>
        /// Refreshes the tool's data for accurate reading.
        /// </summary>
        /// <param name="model">The asset that will be spawned by the tool.</param>
        /// <param name="silhouette">The Model Silhouette based on which the asset will be spawned.</param>
        public void Refresh(ModelAsset model, ModelSilhouette silhouette)
        {
            this.activeModel = model;
            this.silhouette = silhouette;
        }
        
        public void ApplyEffect()
        {
            if (silhouette.IsBlocked) return;
            WorldEditorOverseer.Instance.AddModel(activeModel, silhouette.GetTransformInfo());
        }
    }
}