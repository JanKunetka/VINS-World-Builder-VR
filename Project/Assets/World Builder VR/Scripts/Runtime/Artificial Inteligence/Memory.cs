using System.Linq;
using BoubakProductions.Core;
using UnityEngine;

namespace DoubleMasters.WorldBuilderVR.Systems.Catalog.AI
{
    /// <summary>
    /// Is the memory for recommended asset agents. Contains previous decisions. Used for training.
    /// </summary>
    public class Memory : MonoBehaviour
    {
        [SerializeField] private int memorySize = 5;
        
        private int[] memory;
        private int currentPos;

        private void Awake()
        {
            memory = new int[memorySize];
        }

        /// <summary>
        /// Registers an index into the memory.
        /// </summary>
        /// <param name="index">The index to register.</param>
        public void Register(int index)
        {
            if (memory.Any(t => t == index)) return;
            
            memory[currentPos] = index;
            currentPos++;

            currentPos = IntUtils.Wrap(currentPos, 0, memory.Length);
        }
        
        /// <summary>
        /// Checks if a given index is registered in teh memory.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <returns>True if is in memory.</returns>
        public bool IsIn(int index)
        {
            return memory.Any(pos => index == pos);
        }
    }
}