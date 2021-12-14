using BoubakProductions.Safety;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace WorldBuilderVR.Systems.GASExtension
{
    /// <summary>
    /// Handles input from the button component via the GAS System.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class InteractableButton : MonoBehaviour, IInteractableButton
    {
        [SerializeField] private ButtonType action;
        [SerializeField] private int number = -1;
        
        private Button button;

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClicked);
        }

        public void OnButtonClicked()
        {
            switch (action)
            {
                case ButtonType.DoNothing:
                    Debug.LogError("This button Currently does nothing...");
                    break;

                case ButtonType.ExitGame:
                    GASButtonActions.ExitGame();
                    break;
                
                #region Return from Menus
                case ButtonType.ReturnToMainMenu:
                    GASButtonActions.ReturnToMainMenu();
                    break;
                #endregion

                #region Open Selection Menus
                case ButtonType.SelectionOpenWorld:
                    GASButtonActions.OpenWorldSelection();
                    break;
                #endregion

                #region Create Assets
                case ButtonType.CreateWorld:
                    GASButtonActions.CreateWorld();
                    break;
                
                #endregion

                #region Delete Assets
                case ButtonType.DeleteWorld:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - REMOVE PACK");
                    GASButtonActions.RemoveWorld(number);
                    break;
                #endregion

                #region Open Editors
                case ButtonType.EditorOpenWorld:
                    SafetyNet.EnsureIntIsNotEqual(number, -1, "BUTTON INTERACTION - OPEN WORLD EDITOR");
                    GASButtonActions.OpenWorldEditor(number);
                    break;
                #endregion

                #region Save Editor Changes
                case ButtonType.SaveChangesWorld:
                    GASButtonActions.SaveChangesWorld();
                    break;
                #endregion

                #region Cancel Editor Changes
                case ButtonType.CancelChangesWorld:
                    GASButtonActions.CancelChangesWorld();
                    break;
                #endregion

                #region World Editor

                case ButtonType.SaveChangesWithoutQuit:
                    GASButtonActions.SaveChangesWorldWithoutQuit();
                    break;
                case ButtonType.ResumeFromPause:
                    GASButtonActions.ResumeFromPause();
                    break;

                #endregion
                
                default:
                    throw new InvalidEnumArgumentException("Unknown Button Type.");
            }
        }

        public int Number { get => number; set => number = value; }
    }
}