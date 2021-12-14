using UnityEngine;
using WorldBuilderVR.Ui.AssetSelection.Data;

namespace WorldBuilderVR.Systems.GASExtension
{
    /// <summary>
    /// Takes an ID from <see cref="AssetCardController"/> and sends it to an <see cref="InteractableButton"/>.
    /// </summary>
    [RequireComponent(typeof(InteractableButton))]
    public class InteractableButtonIDSenderCard : MonoBehaviour
    {
        [SerializeField] private AssetCardController assetController;

        private void Start()
        {
            GetComponent<InteractableButton>().Number = assetController.ID;
        }
    }
}