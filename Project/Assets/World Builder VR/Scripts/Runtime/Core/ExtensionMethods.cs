using UnityEngine;

namespace WorldBuilderVR.Core
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Round values of a Vector3.
        /// </summary>
        public static Vector3 Round(this Vector3 vector3, int decimalPlaces = 0)
        {
            float multiplier = 1;
            for (int i = 0; i < decimalPlaces; i++)
            {
                multiplier *= 10f;
            }
            return new Vector3(
                Mathf.Round(vector3.x * multiplier) / multiplier,
                Mathf.Round(vector3.y * multiplier) / multiplier,
                Mathf.Round(vector3.z * multiplier) / multiplier);
        }

        /// <summary>
        /// Works like Mathf.Sign but with a 0.
        /// </summary>
        public static float Sign0(this float number)
        {
            return number == 0 ? 0 : number > 0 ? 1 : -1;
        }
    }
}