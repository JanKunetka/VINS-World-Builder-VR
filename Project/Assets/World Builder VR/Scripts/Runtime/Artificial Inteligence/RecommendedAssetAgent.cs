using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using WorldBuilderVR.Editors.Core;
using WorldBuilderVR.Ui.Catalog;

namespace DoubleMasters.WorldBuilderVR.Systems.Catalog.AI
{
    /// <summary>
    /// Observes user's actions in the catalog menu, and recommends connected assets.
    /// </summary>
    [RequireComponent(typeof(CatalogItem))]
    public class RecommendedAssetAgent : Agent
    {
        [SerializeField] private CatalogItem item;
        [SerializeField] private Memory memory;
        
        private EditModeOverseer editor;
        private int badClicks;
        private int wantedIndex;
    
        private void Start()
        {
            editor = EditModeOverseer.Instance;
            item.Construct(0, editor.CurrentPack.Models[0]);
            item.ActivationButton.onClick.AddListener(PickRecommended);
            CatalogItem.OnAssetActivate += DecidePickQuality;
        }

        public override void OnEpisodeBegin()
        {
            badClicks = 0;
            RequestDecision();
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(badClicks);
            sensor.AddObservation((int)item.Asset.Category);
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            ReconstructItem(actions.DiscreteActions[0]);
            wantedIndex = Random.Range(0, editor.CurrentPack.Models.Length);
        }
    
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            ActionSegment<int> actions = actionsOut.DiscreteActions; 
            actions[0] = Random.Range(0, editor.CurrentPack.Models.Length);
        }

        /// <summary>
        /// Decides, if the clicked asset is good for recommendation or not.
        /// </summary>
        /// <param name="assetIndex"></param>
        public void DecidePickQuality(int assetIndex)
        {
            if (memory.IsIn(assetIndex))
            {
                AddReward(-1f);
                PickedBad();
                return;
            }
            if (editor.CurrentPack.Models[wantedIndex].Category == editor.CurrentPack.Models[assetIndex].Category)
            {
                AddReward(0.2f);
            }
            if (item.Asset.Category == editor.CurrentPack.Models[assetIndex].Category)
            {
                PickGood();
                memory.Register(assetIndex);
                return;
            }
            PickedBad();
        }

        /// <summary>
        /// Ends the Episode on the best end possible.
        /// </summary>
        private void PickRecommended()
        {
            AddReward(1);
            EndEpisode();
        }
        
        /// <summary>
        /// Ends the episode on positive end.
        /// </summary>
        private void PickGood()
        {
            AddReward(0.5f);
            EndEpisode();
        }

        /// <summary>
        /// Ends the episode on a bad end.
        /// </summary>
        private void PickedBad()
        {
            AddReward(-0.5f);
            badClicks++;
            RequestDecision();
        }
        
        /// <summary>
        /// Constructs a new item.
        /// </summary>
        /// <param name="index">The index of the item in the catalog.</param>
        private void ReconstructItem(int index)
        {
            item.Construct(index ,editor.CurrentPack.Models[index]);
        }
    
        public CatalogItem Item {get => item;}
    }
}