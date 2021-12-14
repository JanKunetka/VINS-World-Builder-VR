using UnityEngine;
using WorldBuilderVR.InputSystem;

namespace WorldBuilderVR.Systems.WorldPlacer.ObjectSilhouette
{
    /// <summary>
    /// Contains player input data for positioning and rotating.
    /// </summary>
    public class InputSatchel
    {
        private readonly Transform referenceTransform;
        private readonly Transform referenceOrbitTransform;
        private readonly RollerValueAction scrollRoller;
        private readonly HoldValueAction orbitHolder;

        public InputSatchel(Transform referenceTransform, Transform referenceOrbitTransform, RollerValueAction scrollRoller, HoldValueAction orbitHolder)
        {
            this.referenceTransform = referenceTransform;
            this.referenceOrbitTransform = referenceOrbitTransform;
            this.scrollRoller = scrollRoller;
            this.orbitHolder = orbitHolder;
        }

        public Transform ReferenceTransform { get => referenceTransform;}
        public RollerValueAction ScrollRoller { get => scrollRoller;}
        public HoldValueAction OrbitHolder { get => orbitHolder; }
        public Transform ReferenceOrbitTransform {get => referenceOrbitTransform;}
    }
}