using UnityEngine;
using WorldBuilderVR.Editors.Core.Defaults;
using WorldBuilderVR.Editors.Data.Core;

namespace WorldBuilderVR.Editors.Stages
{
    /// <summary>
    /// An Asset holding information about a specific world the player can visit.
    /// </summary>
    [CreateAssetMenu(fileName = "New Stage Asset", menuName = "World Builder VR/Stage Asset", order = 2)]
    public class StageAsset : AssetBaseSO
    {
        [SerializeField] private GameObject stage;
        [SerializeField] private Vector3 startingPosition;
        [SerializeField] private Vector3 startingRotation;
        
        private void Awake()
        {
            GenerateID(EditorAssetIDs.StageIdentifier);
        }
        
        public GameObject Stage { get => stage; }
        public Vector3 StartingPosition { get => startingPosition; }
        public Vector3 StartingRotation { get => startingRotation; }
    }
}