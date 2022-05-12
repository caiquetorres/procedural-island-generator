using System;
using UnityEngine;

namespace _Project.Map.Scripts
{
    internal class Orbit : MonoBehaviour
    {
        #region Internal methods

        /// <summary>
        /// Method that increases or decreases the distance between the camera and the scenario.
        /// </summary>
        /// <param name="zoom">Defines the zoom amount.</param>
        internal void ApplyZoom(float zoom) =>
            _targetDistance = Math.Clamp(
                _targetDistance - zoom * settings.ZoomSensitivity,
                settings.MinMaxZoomDistance.x,
                settings.MinMaxZoomDistance.y);

        /// <summary>
        /// Method that rotate the camera around some target based on the delta parameter.
        /// </summary>
        /// <param name="delta">Defines a vector that informs how the user is dragging the panel.</param>
        internal void ApplyDelta(Vector2 delta)
        {
            var targetXRotation = _targetRotation.x + -delta.y * settings.OrbitSensitivity;
            var targetYRotation = _targetRotation.y + delta.x * settings.OrbitSensitivity;

            if (settings.RotateAroundY)
            {
                _targetRotation.x = Mathf.Clamp(
                    targetXRotation,
                    settings.MinMaxYRotation.x,
                    settings.MinMaxYRotation.y);
            }
            else
            {
                _targetRotation.x = targetXRotation;
            }

            if (settings.RotateAroundX)
            {
                _targetRotation.y = Mathf.Clamp(
                    targetYRotation,
                    settings.MinMaxXRotation.x,
                    settings.MinMaxXRotation.y);
            }
            else
            {
                _targetRotation.y = targetYRotation;
            }
        }

        #endregion

        #region Private methods

        #region Lifecycle

        private void Awake() => _transform = transform;

        private void Start()
        {
            _targetRotation = _transform.eulerAngles;
            _distance = _targetDistance = settings.StartZoomDistance;
        }

        private void LateUpdate()
        {
            _transform.rotation = Quaternion.Lerp(
                _transform.rotation,
                Quaternion.Euler(_targetRotation),
                settings.OrbitSmoothness * Time.smoothDeltaTime);

            _distance = Mathf.Lerp(
                _distance,
                _targetDistance,
                settings.ZoomSmoothness * Time.deltaTime);

            _transform.position = targetTransform.position - _transform.forward * _distance;
        }

        #endregion

        #endregion

        private float _distance;
        private float _targetDistance;
        private Vector3 _targetRotation;

        private Transform _transform;

        [SerializeField] private CameraSettingsSO settings;

        [Space, SerializeField] private Transform targetTransform;
    }
}
