using BoubakProductions.Core;
using WorldBuilderVR.Systems.GASExtension;

namespace WorldBuilderVR.Core
{
    /// <summary>
    /// Initializes the Main Menu.
    /// </summary>
    public class MainMenuInitializer : MonoSingleton<MainMenuInitializer>
    {
        private void Start()
        {
            GASButtonActions.GameStart();
        }
    }
}