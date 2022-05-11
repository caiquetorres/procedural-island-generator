using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Project._Core.Scripts
{
    /// <summary>
    /// Scriptable object that represents an event.
    /// </summary>
    public abstract class GameEventSO : ScriptableObject
    {
        #region Public methods

        /// <summary>
        /// Method that raises the event based on a string, that will be converted to the event type.
        /// </summary>
        /// <param name="rawValue">Defines the object.</param>
        public abstract void RaiseRaw(string rawValue);

        #endregion
    }

    /// <summary>
    /// Scriptable object that represents a typed event.
    /// </summary>
    /// <typeparam name="T">Defines the type of the object passed through the event.</typeparam>
    public abstract class GameEventSO<T> : GameEventSO
    {
        #region Public events

        /// <summary>
        /// Event that called whenever the event is raised.
        /// </summary>
        public event UnityAction<T> Performed;

        #endregion

        #region Public methods

        /// <summary>
        /// Method that raises the event passing a string, that will be converted to the event type.
        /// </summary>
        /// <param name="rawValue">Defines the object.</param>
        public override void RaiseRaw(string rawValue)
        {
            var value = (T) Convert.ChangeType(rawValue, typeof(T));
            Raise(value);
        }

        /// <summary>
        /// Method that raises the event passing the object through it.
        /// </summary>
        /// <param name="value">Defines the object that will be passed.</param>
        public virtual void Raise(T value) => Performed?.Invoke(value);

        #endregion
    }
}
