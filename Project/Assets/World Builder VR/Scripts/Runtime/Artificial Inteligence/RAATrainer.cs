using WorldBuilderVR.Editors.Core;
using WorldBuilderVR.Ui.Catalog;
using UnityEngine;

namespace DoubleMasters.WorldBuilderVR.Systems.Catalog.AI
{
    /// <summary>
    /// Trains the Recommendation menu and tries to make it great again.
    /// </summary>
    public class RAATrainer : MonoBehaviour
    {
        [SerializeField] private RecommendedAssetAgent agent;
        [SerializeField] private int attempts = 3;

        private EditModeOverseer editor;
        private CatalogItem item;
        private int wantedIndex;
        private int attemptsUsed;
        
        private void Start()
        {
            editor = EditModeOverseer.Instance;
            item = agent.Item;
            ResetAttempts();
            
            InvokeRepeating(nameof(PickAsset), 0.1f, 0.1f);
        }

        private void PickAsset()
        {
            if (attemptsUsed >= attempts)
            {
                ResetAttempts();
                return;
            }

            if (editor.CurrentPack.Models[wantedIndex].Category == item.Asset.Category)
            {
                if (wantedIndex == item.Index)
                {
                    agent.Item.ActivationButton.onClick.Invoke();
                    ResetAttempts();
                }
                else agent.DecidePickQuality(item.Index);
            }
            else agent.DecidePickQuality(item.Index);
            
            attemptsUsed++;
        }

        private void ResetAttempts()
        {
            wantedIndex = Random.Range(0, editor.CurrentPack.Models.Length);
            attemptsUsed = 0;
        }
        
    }
}