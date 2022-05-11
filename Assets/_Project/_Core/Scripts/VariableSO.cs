using System;
using UnityEngine;

namespace _Project._Core.Scripts
{
    /// <summary>
    /// Scriptable object that represents a variable.
    /// </summary>
    public abstract class VariableSO : ScriptableObject
    {
        /// <summary>
        /// Defines the main variable value as a string.
        /// </summary>
        public abstract string RawValue { get; set; }
    }

    /// <summary>
    /// Scriptable object responsible for storing a value of any kind.
    /// </summary>
    /// <typeparam name="T">Defines the cached object type.</typeparam>
    public abstract class VariableSO<T> : VariableSO
    {
        /// <summary>
        /// Defines the main variable value.
        /// </summary>
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                gameEventSo.Raise(value);
            }
        }

        /// <summary>
        /// <inheritdoc cref="RawValue"/>
        /// </summary>
        public override string RawValue
        {
            get => value.ToString();
            set
            {
                var converted = (T) Convert.ChangeType(value, typeof(T));
                Value = converted;
            }
        }

        [SerializeField] protected T value;
        [SerializeField] protected GameEventSO<T> gameEventSo;
    }
}
