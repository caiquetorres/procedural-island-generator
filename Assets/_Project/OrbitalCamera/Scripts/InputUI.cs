using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace _Project.OrbitalCamera.Scripts
{
    public class InputUI : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        #region Internal events

        /// <summary>
        /// Event that is called whenever the user drags the panel.
        /// </summary>
        internal event UnityAction<Vector2> DeltaChange;

        /// <summary>
        /// Event that is called when the user scrolls the mouse wheel.
        /// </summary>
        internal event UnityAction<float> Scroll;

        #endregion

        #region Public methods

        #region UI events

        public void OnPointerEnter(PointerEventData eventData) => _isMouseInside = true;

        public void OnPointerExit(PointerEventData eventData) => _isMouseInside = false;

        public void OnDrag(PointerEventData data)
        {
            if (!data.dragging)
            {
                return;
            }

            DeltaChange?.Invoke(data.delta);
        }

        #endregion

        #endregion

        #region Private methods

        #region Lifecycle

        private void Update()
        {
            if (!_isMouseInside)
            {
                return;
            }

            GetScrollInput();
        }

        #endregion

        private void GetScrollInput()
        {
            var scrollDelta = Input.mouseScrollDelta;

            if (scrollDelta == _scrollDelta)
            {
                return;
            }

            _scrollDelta = scrollDelta;
            Scroll?.Invoke(_scrollDelta.y);
        }

        #endregion

        private bool _isMouseInside;
        private Vector2 _scrollDelta;
    }
}
