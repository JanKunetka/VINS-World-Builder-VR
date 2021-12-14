using System.Collections.Generic;
using WorldBuilderVR.Core;

namespace WorldBuilderVR.Editors.Stages
{
    /// <summary>
    /// Contains all the loaded Stage Assets.
    /// </summary>
    public class StageLibrary
    {
        private IList<StageAsset> stages;

        #region Singleton Pattern
        private static StageLibrary instance;
        private static readonly object padlock = new object();
        public static StageLibrary Instance 
        { 
            get
            {
                // lock (padlock)
                {
                    if (instance == null)
                        instance = new StageLibrary();
                    return instance;
                }
            }
        }
        #endregion

        private StageLibrary()
        {
            stages = new List<StageAsset>();
        }

        /// <summary>
        /// Loads the stages list.
        /// </summary>
        /// <param name="stages">The list of stages to load.</param>
        public void Reload(IList<StageAsset> stages)
        {
            this.stages = new List<StageAsset>(stages);
        }

        /// <summary>
        /// Returns a stage with a given ID.
        /// </summary>
        /// <param name="id">The id of stage to get.</param>
        /// <returns></returns>
        public StageAsset GetByID(string id)
        {
            return stages[stages.FindIndexFirst(id)];
        }

        /// <summary>
        /// Returns a copy of the stages list.
        /// </summary>
        public IList<StageAsset> GetCopy => new List<StageAsset>(stages);
    }
}