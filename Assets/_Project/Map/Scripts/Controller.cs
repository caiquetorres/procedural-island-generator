using UnityEngine;

namespace _Project.Map.Scripts
{
    internal class Controller : MonoBehaviour
    {
        #region Private methods

        #region Lifecycle

        private void OnEnable()
        {
            controllerUI.Drag += orbit.ApplyDelta;
            controllerUI.Scroll += orbit.ApplyZoom;
        }

        private void Start() => factory.Create();

        private void OnDisable()
        {
            controllerUI.Drag -= orbit.ApplyDelta;
            controllerUI.Scroll -= orbit.ApplyZoom;
        }

        #endregion

        #endregion

        [SerializeField] private Orbit orbit;
        [SerializeField] private Factory factory;
        [SerializeField] private ControllerUI controllerUI;
    }
}
