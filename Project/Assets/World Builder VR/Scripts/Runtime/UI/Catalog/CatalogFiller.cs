using UnityEngine;
using WorldBuilderVR.Editors.Models;

namespace WorldBuilderVR.Ui.Catalog
{
    /// <summary>
    /// Contains information about what catalog menu will get filled and with what information.
    /// </summary>
    [System.Serializable]
    public class CatalogFiller
    {
        [SerializeField, Tooltip("Models of what category will get filled into this menu.")]
        private CategoryType category;
        [SerializeField, Tooltip("Under which object will this menu be generated.")]
        private Transform content;

        public CategoryType Category { get => category;}
        public Transform Content { get => content;}
    };
}