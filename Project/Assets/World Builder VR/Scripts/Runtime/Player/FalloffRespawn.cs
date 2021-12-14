using UnityEngine;

namespace WorldBuilderVR.User
{
    /// <summary>
    /// If object falls gets below a certain threshold, return him to start point.
    /// </summary>
    public class FalloffRespawn : MonoBehaviour
    {
        [SerializeField] private float limitY = -10;
        [SerializeField] private Transform trackedObject;

        private Vector3 startPos;
        
        private void Start()
        {
            startPos = trackedObject.position;
        }

        private void Update()
        {
            if (trackedObject.position.y <= limitY)
            {
                trackedObject.position = startPos;
            }
        }
    }
}