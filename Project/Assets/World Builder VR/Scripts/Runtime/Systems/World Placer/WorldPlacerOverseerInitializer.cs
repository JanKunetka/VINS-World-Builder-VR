using UnityEngine;
using WorldBuilderVR.Core;
using WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette;
using WorldBuilderVR.Systems.XR;

namespace WorldBuilderVR.Systems.WorldPlacer
{
    /// <summary>
    /// Loads the <see cref="WorldPlacerOverseer"/> with data.
    /// </summary>
    public class WorldPlacerOverseerInitializer : MonoBehaviour, IOverseerInitializer
    {
        [SerializeField] private ModelSilhouette modelSilhouette;
        [SerializeField] private StraightRaycaster toolRaycaster;
        
        private WorldPlacerOverseer overseer;
        
        public void Initialize()
        {
            overseer = WorldPlacerOverseer.Instance;
            overseer.Initialize(modelSilhouette, toolRaycaster);
        }
    }
}