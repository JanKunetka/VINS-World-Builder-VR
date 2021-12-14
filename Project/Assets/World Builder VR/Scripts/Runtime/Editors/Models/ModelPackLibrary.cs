using System.Collections.Generic;
using WorldBuilderVR.Core;

namespace WorldBuilderVR.Editors.Models
{
    /// <summary>
    /// Contains all the loaded Model Packs.
    /// </summary>
    public class ModelPackLibrary
    {
        private IList<ModelPackAsset> modelPacks;

        #region Singleton Pattern
        private static ModelPackLibrary instance;
        private static readonly object padlock = new object();
        public static ModelPackLibrary Instance 
        { 
            get
            {
                // lock (padlock)
                {
                    if (instance == null)
                        instance = new ModelPackLibrary();
                    return instance;
                }
            }
        }
        #endregion

        private ModelPackLibrary()
        {
            modelPacks = new List<ModelPackAsset>();
        }

        /// <summary>
        /// Loads the model packs list.
        /// </summary>
        /// <param name="modelPacks">The model pack list to load.</param>
        public void Reload(IList<ModelPackAsset> modelPacks)
        {
            this.modelPacks = new List<ModelPackAsset>(modelPacks);
        }

        /// <summary>
        /// Returns a model pack with a given ID.
        /// </summary>
        /// <param name="id">The id of the model pack to get.</param>
        /// <returns>The Model Pack.</returns>
        public ModelPackAsset GetByID(string id)
        {
            return modelPacks[modelPacks.FindIndexFirst(id)];
        }
        
        /// <summary>
        /// Returns a copy of the stages list.
        /// </summary>
        public IList<ModelPackAsset> GetCopy => new List<ModelPackAsset>(modelPacks);
    }
}