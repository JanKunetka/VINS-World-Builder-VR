using UnityEngine;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Models;
using WorldBuilderVR.Editors.Worlds;
using WorldBuilderVR.Systems.XR;

namespace WorldBuilderVR.Systems.WorldPlacer.ToolBox
{
    /// <summary>
    /// The Tool used for Removing Objects.
    /// </summary>
    public class RemoverTool : ITool
    {
        private IRaycaster raycaster;
        private ModelAsset lastRemovedModel;

        public void Enable()
        {
            raycaster?.BeginScanning();
        }
        
        public void Disable()
        {
            raycaster?.EndScanning();
        }
        
        /// <summary>
        /// Refreshes the tool's data for accurate reading.
        /// </summary>
        /// <param name="targetTransform">The transform of the object to remove.</param>
        public void Refresh(IRaycaster raycaster)
        {
            this.raycaster = raycaster;
        }
        
        public void ApplyEffect()
        {
            if (raycaster.HitInfo.transform == null) return; 
            if (!raycaster.HitInfo.transform.CompareTag("SpawnedObject")) return;
            
            GameObject hittedObject = raycaster.HitInfo.transform.gameObject;
            lastRemovedModel = hittedObject.GetComponentInParent<SpawnedObjectAsset>().asset;
            
            WorldEditorOverseer.Instance.RemoveModel(lastRemovedModel, TransformPacker.Pack(hittedObject.transform.root));
            GameObject.Destroy(hittedObject);
        }

        public ModelAsset LastRemovedModel { get => lastRemovedModel; }
    }
}