using UnityEngine;
using UnityEngine.Events;

namespace _Project._Core.Scripts
{
    /// <summary>
    /// Scriptable object that represents an event.
    /// </summary>
    [CreateAssetMenu(menuName = "Game Events/void", fileName = "new Void Game Event")]
    public class VoidGameEventSO : ScriptableObject
    {
        #region Public events

        /// <summary>
        /// Event that called whenever the event is raised.
        /// </summary>
        public event UnityAction Performed;

        #endregion

        /// <summary>
        /// Method that raises the event.
        /// </summary>
        public void Raise() => Performed?.Invoke();
    }
}
