using BoubakProductions.Safety;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace WorldBuilderVR.Systems.WorldPlacer.ToolBox.Visualizer
{
    /// <summary>
    /// Visualizes the current active tool.
    /// </summary>
    public class ToolVisualizer : MonoBehaviour
    {
        [SerializeField] private XRInteractorLineVisual lineVisual;
        [SerializeField] private MeshRenderer handRenderer;
        [SerializeField] private VisualInfo[] toolVisuals;

        private Gradient originalGradient;
        private Material originalMaterial;

        private void Start()
        {
            originalGradient = lineVisual.invalidColorGradient;
            originalMaterial = handRenderer.material;
        }

        private void OnEnable()
        {
            ToolboxManager.OnToolChange += ApplyVisuals;
            WorldPlacerOverseer.Instance.OnDisablePlacementMode += ResetMaterial;
        }
        
        private void OnDisable()
        {
            ToolboxManager.OnToolChange -= ApplyVisuals;
            WorldPlacerOverseer.Instance.OnDisablePlacementMode -= ResetMaterial;
        }

        /// <summary>
        /// Assign visuals to the currently active tool.
        /// </summary>
        /// <param name="toolIndex">The index of the newly active tool.</param>
        private void ApplyVisuals(int toolIndex)
        {
            SafetyNet.EnsureIntIsInRange(toolIndex, 0, toolVisuals.Length, "ToolIndex");
            
            lineVisual.invalidColorGradient = toolVisuals[toolIndex].gradient;
            handRenderer.material = toolVisuals[toolIndex].material;
        }

        /// <summary>
        /// Reset hand material to it's original form.
        /// </summary>
        private void ResetMaterial()
        {
            lineVisual.invalidColorGradient = originalGradient;
            handRenderer.material = originalMaterial;
        }

    }
}