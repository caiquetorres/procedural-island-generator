using _Project.Navigation.Scripts;
using UnityEngine;

namespace _Project.Map.Scripts
{
    internal class Controller : SceneController
    {
        #region Private methods

        #region Lifecycle

        private void OnEnable()
        {
            controllerUI.Drag += orbit.ApplyDelta;
            controllerUI.Scroll += orbit.ApplyZoom;
        }

        private void OnDisable()
        {
            controllerUI.Drag -= orbit.ApplyDelta;
            controllerUI.Scroll -= orbit.ApplyZoom;
        }

        #endregion

        #endregion

        [SerializeField] private Orbit orbit;
        [SerializeField] private ControllerUI controllerUI;
    }
}
