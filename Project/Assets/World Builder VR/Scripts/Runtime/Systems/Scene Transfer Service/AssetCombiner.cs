using System;
using System.Collections.Generic;
using UnityEngine;
using WorldBuilderVR.Editors.Models;

namespace WorldBuilderVR.Systems.SceneTransfer
{
    /// <summary>
    /// Handles combining of assets.
    /// </summary>
    public class AssetCombiner
    {
        /// <summary>
        /// Combines multiple Model packs into one singular Model Pack.
        /// </summary>
        /// <param name="packs">The list of Model Packs to combine.</param>
        /// <returns>A Model Pack containing all models from the previous ones.</returns>
        public ModelPackAsset Combine(IEnumerable<ModelPackAsset> packs)
        {
            ModelPackAsset ultimatePack = ScriptableObject.CreateInstance<ModelPackAsset>();
            ultimatePack.UpdateModels(Array.Empty<ModelAsset>());
            foreach (ModelPackAsset pack in packs)
            {
                ModelAsset[] combinedModels = new ModelAsset[ultimatePack.Models.Length + pack.Models.Length];
                Array.Copy(ultimatePack.Models, combinedModels, ultimatePack.Models.Length);
                Array.Copy(pack.Models, 0, combinedModels, ultimatePack.Models.Length, pack.Models.Length);
                ultimatePack.UpdateModels(combinedModels);
            }
            return ultimatePack;
        }
    }
}