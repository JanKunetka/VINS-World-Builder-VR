using UnityEngine;

namespace WorldBuilderVR.User
{
    /// <summary>
    /// Handles Menu visibility.
    /// </summary>
    public class MenuVisibilityController : MonoBehaviour
    {
        [SerializeField] private GameObject menuObject;

        /// <summary>
        /// Switches the active state.
        /// </summary>
        public void SwitchMenuStatus()
        {
            menuObject.SetActive(!menuObject.activeSelf);
        }
        
    }
}