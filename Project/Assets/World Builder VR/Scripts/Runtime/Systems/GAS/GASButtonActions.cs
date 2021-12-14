using System;
using BoubakProductions.GASCore;
using BoubakProductions.UI.MenuSwitching;
using UnityEngine;
using WorldBuilderVR.Core;
using WorldBuilderVR.Editors.Core;
using WorldBuilderVR.Editors.Worlds;
using WorldBuilderVR.InputSystem.Registries;
using WorldBuilderVR.Systems.SceneTransfer;

namespace WorldBuilderVR.Systems.GASExtension
{
    /// <summary>
    /// Contains methods, that will run based on pressing UI buttons in the game.
    /// </summary>
    public static class GASButtonActions
    {
        public static void GameStart()
        {
            
        }
        
        public static void ExitGame()
        {
            Application.Quit();
        }
        
        #region Create Assets
        public static void CreateWorld()
        {
            new WorldBuilder().Build();
            GASWorldVR.ReopenSelectionMenu(AssetType.World);
        }

        #endregion

        #region Delete Assets
        public static void RemoveWorld(int worldIndex)
        {
            WorldLibrary.Instance.DeleteWorld(worldIndex);
            GASWorldVR.ReopenSelectionMenu(AssetType.World);
        }

        #endregion

        #region Return From Menus

        public static void ReturnToMainMenu()
        {
            GAS.SwitchMenu(MenuType.MainMenu);
        }

        #endregion

        #region Open Editors

        public static void OpenWorldEditor(int worldIndex)
        {
            SceneTransferService.GetInstance().LoadUp(worldIndex);
            GAS.SwitchScene(1);
        }

        #endregion

        #region Open Selection Menus

        public static void OpenWorldSelection()
        {
            GAS.SwitchMenu(MenuType.AssetSelection);
            GASWorldVR.ReopenSelectionMenu(AssetType.World);
        }

        #endregion

        #region Save Changes

        public static void SaveChangesWorld()
        {
            SaveChangesWorldWithoutQuit();
            EditModeOverseer.Instance.SwitchModeToNone();
            WorldEditorOverseer.Instance.CompleteEditing();
            GAS.SwitchScene(0);
        }

        #endregion

        #region Cancel Changes

        public static void CancelChangesWorld()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region World Editor

        public static void ResumeFromPause()
        {
            InputRegistryExplore.GetInstance().PauseMenuController.SwitchMenuStatus();
        }
        
        public static void SaveChangesWorldWithoutQuit()
        {
            WorldEditorOverseer.Instance.SaveChanges();
        }

        #endregion

        
    }
}