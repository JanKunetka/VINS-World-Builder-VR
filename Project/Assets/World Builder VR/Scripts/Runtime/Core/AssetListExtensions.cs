using System.Collections.Generic;
using System.Linq;
using BoubakProductions.Safety;
using WorldBuilderVR.Editors.Data.Core;

namespace WorldBuilderVR.Core
{
    /// <summary>
    /// Contains various helpful methods for working with <see cref="IAssetBase"/> lists. 
    /// </summary>
    public static class AssetListExtensions
    {
        /// <summary>
        /// Finds the index of the first asset with the same ID.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="asset">The asset to compare to.</param>
        /// <typeparam name="T">Type is <see cref="IAssetBase"/> or any of it's children.</typeparam>
        /// <returns>Index of the first found asset.</returns>
        /// <exception cref="SafetyNetCollectionException">Is thrown when no asset with ID was found.</exception>
        public static int FindIndexFirst<T>(this IList<T> list, IAssetBase asset) where T : IAssetBase
        {
            return FindIndexFirst(list, asset.ID);
        }
        
        /// <summary>
        /// Finds the index of the first asset with the same ID.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="id">The ID of the asset to search for.</param>
        /// <typeparam name="T">Type is <see cref="IAssetBase"/> or any of it's children.</typeparam>
        /// <returns>Index of the first found asset.</returns>
        /// <exception cref="SafetyNetCollectionException">Is thrown when no asset with ID was found.</exception>
        public static int FindIndexFirst<T>(this IList<T> list, string id) where T : IAssetBase
        {
            SafetyNet.EnsureIsNotNull(list, "List ot search");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == id)
                {
                    return i;
                }
            }

            throw new SafetyNetCollectionException($"No asset with the ID '{id}' was not found in the list.");
        }

        /// <summary>
        /// Tries to find and copy entries from one list to another.
        /// </summary>
        /// <param name="list">The list to search on for same assets.</param>
        /// <param name="ids">The list asset ids which will be used to search for.</param>
        /// <typeparam name="T">Type is <see cref="IAssetBase"/> or any of it's children.</typeparam>
        /// <returns>A List of the found assets.</returns>
        public static IList<T> GrabFromList<T>(this IList<T> list, IList<string> ids) where T : IAssetBase
        {
            IList<T> foundAssets = new List<T>();
            IList<string> idList = new List<string>(ids);
            for (int i = 0; i < list.Count; i++)
            {
                if (idList.Count <= 0) break;
                for (int j = 0; j < idList.Count; j++)
                {
                    if (idList[j] == list[i].ID)
                    {
                        foundAssets.Add(list[i]);
                        idList.RemoveAt(j);
                        break;
                    }
                }
            }

            return foundAssets;
        }

        /// <summary>
        /// Converts a list of assets into a list of their IDs.
        /// </summary>
        /// <param name="assets">The list of assets to convert</param>
        /// <typeparam name="T">Type is <see cref="IAssetBase"/> or any of it's children.</typeparam>
        /// <returns>A list of IDs (strings).</returns>
        public static IList<string> ConvertToIDs<T>(this IList<T> assets) where T : IAssetBase
        {
            return assets.Select(asset => asset.ID).ToList();
        }

        /// <summary>
        /// Converts a list of <see cref="IAssetBase"/> children into a list of their parent.
        /// </summary>
        /// <param name="list">The list to convert</param>
        /// <typeparam name="T">Should be children of <see cref="IAssetBase"/>. Otherwise will skip conversion.</typeparam>
        /// <returns>The list converted fully to <see cref="IAssetBase"/>.</returns>
        public static IList<IAssetBase> ConvertToIAssetBase<T>(this IList<T> list) where T : IAssetBase
        {
            if (typeof(T) == typeof(IAssetBase)) return (IList<IAssetBase>)list;
            return list.Cast<IAssetBase>().ToList();
        }
    }
}