using UnityEngine;

namespace _Project.Map.Scripts
{
    [CreateAssetMenu(menuName = "Map/Camera/Settings", fileName = "new Camera Settings")]
    public class CameraSettingsSO : ScriptableObject
    {
        #region Internal properties

        /// <summary>
        /// Property that defines if the camera should validate the rotation in the X axis.
        /// </summary>
        internal bool RotateAroundX => rotateAroundX;

        /// <summary>
        /// Property that defines if the camera should validate the rotation in the Y axis.
        /// </summary>
        internal bool RotateAroundY => rotateAroundY;

        /// <summary>
        /// Property that defines the orbit sensitivity.
        /// </summary>
        internal float OrbitSensitivity => orbitSensitivity;

        /// <summary>
        /// Property that defines the orbit smoothness.
        /// </summary>
        internal float OrbitSmoothness => orbitSmoothness;

        /// <summary>
        /// Property that defines the start zoom distance.
        /// </summary>
        internal float StartZoomDistance => startZoomDistance;

        /// <summary>
        /// Method that defines the zoom sensitivity.
        /// </summary>
        internal float ZoomSensitivity => zoomSensitivity;

        /// <summary>
        /// Method that defines the zoom smoothness.
        /// </summary>
        internal float ZoomSmoothness => zoomSmoothness;

        /// <summary>
        /// Method that defines the minimum and the maximum rotation in X axis.
        /// </summary>
        internal Vector2Int MinMaxXRotation => minMaxXRotation;

        /// <summary>
        /// Method that defines the minimum and the maximum rotation in Y axis.
        /// </summary>
        internal Vector2Int MinMaxYRotation => minMaxYRotation;

        /// <summary>
        /// Method that defines the minimum and the maximum zoom distance.
        /// </summary>
        internal Vector2 MinMaxZoomDistance => minMaxZoomDistance;

        #endregion

        [Header("Orbit"), Min(0.1f), SerializeField]
        private float orbitSensitivity = .1f;

        [Min(0.01f), SerializeField] private float orbitSmoothness = 10f;

        [Space, SerializeField] private bool rotateAroundX;
        [SerializeField] private Vector2Int minMaxXRotation = new(-90, 90);

        [Space, SerializeField] private bool rotateAroundY = true;
        [SerializeField] private Vector2Int minMaxYRotation = new(-45, 45);

        [Space, Header("Zoom"), Min(.1f), SerializeField]
        private float startZoomDistance = 3f;

        [SerializeField] private Vector2 minMaxZoomDistance = new(1.5f, 7f);
        [Min(.01f), SerializeField] private float zoomSensitivity = .25f;
        [Min(0.01f), SerializeField] private float zoomSmoothness = 10f;
    }
}
