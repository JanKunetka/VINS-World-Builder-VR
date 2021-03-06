namespace BoubakProductions.UI.MenuSwitching
{
    public interface IMenuSwitcher
    {
        /// <summary>
        /// Switches from the current menu to a new one.
        /// </summary>
        /// <param name="menu">The new menu to switch to.</param>
        public void SwitchTo(MenuType menu);
    }
}