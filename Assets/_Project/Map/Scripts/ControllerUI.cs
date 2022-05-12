using UnityEngine;
using UnityEngine.Events;

namespace _Project.Map.Scripts
{
    internal class ControllerUI : MonoBehaviour
    {
        #region Internal events

        /// <summary>
        /// Event that is called whenever the user drags the panel.
        /// </summary>
        internal event UnityAction<Vector2> Drag
        {
            add => input.DeltaChange += value;
            remove => input.DeltaChange -= value;
        }

        /// <summary>
        /// Event that is called when the user scrolls the mouse wheel.
        /// </summary>
        public event UnityAction<float> Scroll
        {
            add => input.Scroll += value;
            remove => input.Scroll -= value;
        }

        #endregion

        [SerializeField] private InputUI input;
    }
}
