
namespace WorldBuilderVR.Systems.GASExtension
{
    public enum ButtonType
    {
        DoNothing = 0,
        ExitGame = 10,

        #region Open Selection Menus
        SelectionOpenWorld = 1,

        #endregion

        #region Return from Menus
        ReturnToMainMenu = 2,
        #endregion

        #region Create Assets
        CreateWorld = 3,
        #endregion

        #region Delete Assets
        DeleteWorld = 4,
        #endregion

        #region Open Editors
        EditorOpenWorld = 5,
        #endregion

        #region Save Editor Changes
        SaveChangesWorld = 6,
        #endregion

        #region Cancel Editor Changes
        CancelChangesWorld = 7,
        #endregion

        #region World Editor

        SaveChangesWithoutQuit = 8,
        ResumeFromPause = 9,

        #endregion
    }   
}