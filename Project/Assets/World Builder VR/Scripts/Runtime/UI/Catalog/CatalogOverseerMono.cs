using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WorldBuilderVR.Editors.Models;

namespace WorldBuilderVR.Ui.Catalog
{
    /// <summary>
    /// Overseers the catalog menu.
    /// </summary>
    public class CatalogOverseerMono : MonoBehaviour
    {
        [SerializeField, Tooltip("All the different menus, that will be filled with objects.")]
        private CatalogFiller[] menus;

        [SerializeField, Tooltip("The base for the assets, that will fill the menus.")]
        private CatalogItem itemPrefab;

        /// <summary>
        /// Fills all menus with correct objects.
        /// </summary>
        public void Fill(ModelPackAsset pack)
        {
            foreach (CatalogFiller catalog in menus)
            {
                ReferencedAsset[] models = Gather(pack.Models, catalog.Category);
                foreach (ReferencedAsset model in models)
                {
                    SpawnButton(catalog.Content, model);
                }
            }
        }

        /// <summary>
        /// Gathers all models with the same category in an array.
        /// </summary>
        /// <param name="models">The array of models to search.</param>
        /// <param name="category">The category we are interested in.</param>
        /// <returns>An array with found models.</returns>
        private ReferencedAsset[] Gather(ModelAsset[] models, CategoryType category)
        {
            IList<ReferencedAsset> gatheredModels = new List<ReferencedAsset>();
            for (int i = 0; i < models.Length; i++)
            {
                if (models[i].Category != category && category != CategoryType.NotCategorized) continue;
                gatheredModels.Add(new ReferencedAsset(models[i], i));
            }
            return gatheredModels.ToArray();
        }

        /// <summary>
        /// Spawns an "Asset Create" button with correct data.
        /// </summary>
        /// <param name="parent">The button will be spawned under this object.</param>
        /// <param name="model">The data the button will inhibit.</param>
        private void SpawnButton(Transform parent, ReferencedAsset model)
        {
            CatalogItem item = Instantiate(itemPrefab, parent).GetComponent<CatalogItem>();
            item.Construct(model.Index, (ModelAsset)model.Asset);
        }
        
    }
}