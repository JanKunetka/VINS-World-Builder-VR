using UnityEngine;
using WorldBuilderVR.Core;
using WorldBuilderVR.InputSystem;

namespace WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette.TransformOverseer
{
    public class SilhouetteTransformOverseerInitializer : MonoBehaviour, IOverseerInitializer
    {
        [SerializeField] private ModelSilhouette modelSilhouette;
        [SerializeField] private Transform referenceTransform;
        [SerializeField] private Transform referenceOrbitTransform;
        [Header("Input")]
        [SerializeField] private RollerValueAction scrollRoller;
        [SerializeField] private HoldValueAction orbitHolder;

        public void Initialize()
        {
            InputSatchel satchel = new InputSatchel(referenceTransform, referenceOrbitTransform, scrollRoller, orbitHolder);
            SilhouetteTransformOverseer.Instance.Initialize(modelSilhouette, satchel);
        }
    }
}