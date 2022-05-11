using UnityEngine;

namespace _Project.Navigation.Scripts
{
    /// <summary>
    /// Abstract class that represents a scene manager, responsible for all of the scene complex logic. Such as API consuming and navigation.
    /// </summary>
    public abstract class SceneController : MonoBehaviour
    {
    }

    /// <summary>
    /// Abstract class that represents a scene manager, responsible for all of the scene complex logic. Such as API consuming and navigation.
    /// </summary>
    /// <typeparam name="T">Defines the state type.</typeparam>
    public abstract class SceneController<T> : SceneController
        where T : struct
    {
        #region Public properties

        /// <summary>
        /// Property that defines the scene state.
        /// </summary>
        public T Props
        {
            get => props;
            set => props = value;
        }

        #endregion

        [SerializeField] private T props;
    }
}
