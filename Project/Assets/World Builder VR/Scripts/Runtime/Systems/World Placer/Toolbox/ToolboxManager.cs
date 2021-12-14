using System;
using BoubakProductions.Core;
using WorldBuilderVR.Editors.Models;
using WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette;
using WorldBuilderVR.Systems.XR;

namespace WorldBuilderVR.Systems.WorldPlacer.ToolBox
{
    /// <summary>
    /// Manages which tool is currently selected, and applies it's effects.
    /// </summary>
    public class ToolboxManager
    {
        private const int ToolsAmount = 2;
        
        public static event Action<int> OnToolChange;
        
        private readonly IRaycaster raycaster;
        private readonly ModelSilhouette modelSilhouette;
        
        private readonly PlacerTool placer;
        private readonly RemoverTool remover;
        
        private ITool currentTool;
        private int currentIndex;

        public ToolboxManager(IRaycaster raycaster, ModelSilhouette modelSilhouette)
        {
            this.raycaster = raycaster;
            this.modelSilhouette = modelSilhouette;

            placer = new PlacerTool();
            remover = new RemoverTool();
            
            ChooseFirst();
        }

        /// <summary>
        /// Refreshes data on all the tools.
        /// </summary>
        public void Refresh(ModelAsset currentAsset)
        {
            placer.Refresh(currentAsset, modelSilhouette);
            remover.Refresh(raycaster);
        }

        /// <summary>
        /// Chooses the first tool.
        /// </summary>
        public void ChooseFirst()
        {
            currentIndex = 2;
            Cycle();
        }
        
        /// <summary>
        /// Cycles to the next tool.
        /// </summary>
        public void Cycle()
        {
            remover.Disable();
            
            currentIndex++;
            currentIndex = IntUtils.Wrap(currentIndex, 0, ToolsAmount-1);
            switch (currentIndex)
            {
                case 0:
                    modelSilhouette.Enable();
                    currentTool = placer;
                    break;
                case 1:
                    modelSilhouette.Disable();
                    remover.Enable();
                    currentTool = remover;
                    break;
            }
            
            OnToolChange?.Invoke(currentIndex);
        }
        
        /// <summary>
        /// Apply the given tool effect.
        /// </summary>
        public void ApplyEffect()
        {
            currentTool.ApplyEffect();
        }
    }
}