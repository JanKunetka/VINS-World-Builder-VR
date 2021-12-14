using UnityEngine;

namespace WorldBuilderVR.InputSystem
{
    /// <summary>
    /// Calculates a value via scrolling.
    /// </summary>
    public class RollerValueAction : MonoBehaviour
    {
        [SerializeField] private float minValue;
        [SerializeField] private float maxValue;
        [SerializeField] private float baseIncrement;
        [SerializeField] private float lerpSpeed;
        
        private float value;
        
        /// <summary>
        /// Recalculates the value based on input.
        /// </summary>
        /// <param name="inputAxis">The input axis to base the value change.</param>
        public void Recalculate(float inputAxis)
        {
            value = Mathf.Max(minValue, value);
            value = Mathf.Min(value, maxValue);

            float targetValue = value + (inputAxis * baseIncrement);
            value = Mathf.Lerp(value, targetValue, Time.deltaTime * lerpSpeed);
        }

        public float Value {get => value;}
    }
}