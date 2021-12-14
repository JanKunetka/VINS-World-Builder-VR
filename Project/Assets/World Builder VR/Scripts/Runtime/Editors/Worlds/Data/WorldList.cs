using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoubakProductions.Safety;
using WorldBuilderVR.Editors.Worlds;
using WorldBuilderVR.Systems.ExternalStorage;

namespace WorldBuilderVR.Editors.Worlds
{
    /// <summary>
    /// Special distinct List variant for worlds. Is synced with external storage.
    /// </summary>
    public class WorldList : IList<WorldAsset>
    {
        private IList<WorldAsset> list;

        public WorldList()
        {
            list = new List<WorldAsset>();
        }
        public WorldList(WorldList original)
        {
            list = new List<WorldAsset>(original);
        }

        /// <summary>
        /// Adds a new world to the library.
        /// </summary>
        /// <param name="world">The WorldAsset to add.</param>
        public void Add(WorldAsset world)
        {
            if (TryFinding(world.Title, world.Author) != null)
                throw new FoundDuplicationException("You are trying to create a world, that already exists. Cannot have the same title and author!");
            
            ExternalStorageOverseer.Instance.Save(world);
            list.Add(world);
        }
        
        /// <summary>
        /// Change an world on a specific index in the list.
        /// </summary>
        /// <param name="index">Index of the asset to change.</param>
        /// <param name="world">The new world data.</param>
        /// <exception cref="FoundDuplicationException"></exception>
        public void Update(int index, WorldAsset world)
        {
            SafetyNet.EnsureIsNotNull(world, "world to add");
            SafetyNet.EnsureIntIsInRange(index ,0, list.Count, "world List");
            
            if (TryFinding(world.Title, world.Author, index) != null)
                throw new FoundDuplicationException("You are trying to update a world with a name and author that is already taken. Cannot have the same title and author!");
            list[index] = world;
            
            ExternalStorageOverseer.Instance.Save(world);
        }

        /// <summary>
        /// Remove world from the library with a specific name.
        /// </summary>
        /// <param name="name">The name of the world to remove.</param>
        public void Remove(string name, string author)
        {
            WorldAsset foundWorld = TryFinding(name, author);
            SafetyNet.EnsureIsNotNull(foundWorld, "world to Remove");

            ExternalStorageOverseer.Instance.DeleteWorld(foundWorld);
            list.Remove(foundWorld);
        }

        /// <summary>
        /// Remove world from the library with a specific name.
        /// </summary>
        /// <param name="worldIndex">The position of the world to remove on the list.</param>
        public void Remove(int worldIndex)
        {
            SafetyNet.EnsureIntIsInRange(worldIndex, 0, list.Count, "world Index when removing");
            WorldAsset foundWorld = list[worldIndex];
            SafetyNet.EnsureIsNotNull(foundWorld, "world to Remove");

            ExternalStorageOverseer.Instance.DeleteWorld(foundWorld);
            list.Remove(foundWorld);
        }

        /// <summary>
        /// Finds a world in the library by a given name and a given author.
        /// If no world is found, returns NULL.
        /// </summary>
        /// <param name="worldName">The name by which we are searching</param>
        /// <param name="author">The name of the author who created the world.</param>
        /// <param name="excludePos">The position in the list to exclude.</param>
        /// <returns>The world asset with the given name</returns>
        public WorldAsset TryFinding(string worldName, string author, int excludePos = -1)
        {
            if (list.Count == 0) return null;
            IList<WorldAsset> foundWorlds = list.Where((world, counter) => counter != excludePos)
                                              .Where((world) => world.Title == worldName && world.Author == author)
                                              .ToList();

            SafetyNet.EnsureListIsNotLongerThan(foundWorlds, 1, "Found worlds by name & author");
            return (foundWorlds.Count == 0) ? null : foundWorlds[0];
        }

        /// <summary>
        /// Replaces the current list with a new one.
        /// </summary>
        /// <param name="newList">The list to replace it with.</param>
        public void ReplaceAll(IList<WorldAsset> newList)
        {
            list = new List<WorldAsset>(newList);
        }

        #region Untouched Overrides
        public int Count => list.Count;
        public bool IsReadOnly => false;
        public WorldAsset this[int index] { get => list[index]; set => list[index] = value; }

        public int IndexOf(WorldAsset item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, WorldAsset item)
        {
            SafetyNet.EnsureListNotContain(this, item, "List of worlds");
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(WorldAsset item)
        {
            return list.Contains(item);
        }

        public void CopyTo(WorldAsset[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public bool Remove(WorldAsset item)
        {
            return list.Remove(item);
        }

        public IEnumerator<WorldAsset> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
        #endregion
    }
}