using System;
using System.Collections.Generic;
using UnityEngine;
using WorldBuilderVR.Editors.Models;

namespace WorldBuilderVR.Editors.Worlds
{
    /// <summary>
    /// Contains methods, that will help with gathering models from model packs.
    /// </summary>
    public static class ModelPackCombiner
    {
        /// <summary>
        /// Gathers all the models from all packs associated with the world into a single list.
        /// </summary>
        /// <param name="modelPackIDs">The list of Model Pack IDs to gather models from.</param>
        /// <returns>A list of all models available in found model packs.</returns>
        public static IList<ModelAsset> Gather(IList<string> modelPackIDs)
        {
            IList<ModelPackAsset> loadedPacks = new List<ModelPackAsset>();
            List<ModelAsset> allModels = new List<ModelAsset>();
            
            //Gather the model packs.
            foreach (string id in modelPackIDs)
            {
                try
                {
                    loadedPacks.Add(ModelPackLibrary.Instance.GetByID(id));
                }
                catch (Exception)
                {
                    Debug.Log($"Pack with ID '{id}' failed to load.");
                }
            }

            //Put all models into 1 list.
            foreach (ModelPackAsset pack in loadedPacks)
            {
                allModels.AddRange(pack.Models);
            }

            return allModels;
        }
    }
}