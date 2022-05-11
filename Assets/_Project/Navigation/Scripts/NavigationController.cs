using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Navigation.Scripts
{
    /// <summary>
    /// Class is responsible for dealing with all the application navigation business logic.
    /// </summary>
    public class NavigationController : MonoBehaviour
    {
        #region Private static properties

        /// <summary>
        /// Property that defines the unique "NavigationController" application object instance.
        /// </summary>
        private static NavigationController Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }

                var nav = Resources.Load<NavigationController>("_navigationController");
                Instantiate(nav, Vector3.zero, Quaternion.identity);

                return _instance;
            }
        }

        #endregion

        #region Public static methods

        /// <summary>
        /// Method that adds some scene in the application scenes stack.
        /// </summary>
        /// <param name="sceneName">Defines the target scene name.</param>
        public static AsyncOperation Push(string sceneName)
        {
            Instance._scenes.Push(sceneName);
            var operation = Load(sceneName, LoadSceneMode.Additive);
            operation.completed += SortCanvas;
            return operation;
        }

        /// <summary>
        /// Method that adds some scene in the application scenes stack.
        /// </summary>
        /// <param name="data">Defines the initial scene state.</param>
        /// <param name="sceneName">Defines the target scene name.</param>
        public static AsyncOperation Push<T>(T data, string sceneName)
            where T : struct
        {
            Instance._scenes.Push(sceneName);
            var operation = Load(data, sceneName, LoadSceneMode.Additive);
            operation.completed += SortCanvas;
            return operation;
        }

        /// <summary>
        /// Method that redirects the user to some scene unloading all the others and cleaning the stack.
        /// </summary>
        /// <param name="sceneName">Defines the target scene name.</param>
        public static AsyncOperation Navigate(string sceneName)
        {
            Instance._scenes.Clear();
            return Load(sceneName, LoadSceneMode.Single);
        }

        /// <summary>
        /// Method that redirects the user to some scene unloading all the others and cleaning the stack.
        /// </summary>
        /// <param name="data">Defines the initial scene state.</param>
        /// <param name="sceneName">Defines the target scene name.</param>
        public static AsyncOperation Navigate<T>(T data, string sceneName)
            where T : struct
        {
            Instance._scenes.Clear();
            return Load(data, sceneName, LoadSceneMode.Single);
        }

        /// <summary>
        /// Method that unloads the last scene added in the stack.
        /// </summary>
        public static AsyncOperation Pop()
            => Unload(Instance._scenes.Pop());

        /// <summary>
        /// Method that loads some scene.
        /// </summary>
        /// <param name="sceneName">Defines the scene name.</param>
        /// <param name="mode">Defines the scene load mode.</param>
        public static AsyncOperation Load(string sceneName, LoadSceneMode mode)
            => SceneManager.LoadSceneAsync(sceneName, mode);

        /// <summary>
        /// Method that loads some scene.
        /// </summary>
        /// <param name="data">Defines the initial scene state.</param>
        /// <param name="sceneName">Defines the scene name.</param>
        /// <param name="mode">Defines the scene load mode.</param>
        public static AsyncOperation Load<T>(T data, string sceneName, LoadSceneMode mode)
            where T : struct
        {
            var operation = Load(sceneName, mode);
            operation.completed += _ =>
            {
                var controllers = FindObjectsOfType<SceneController<T>>();
                foreach (var controller in controllers)
                {
                    controller.Props = data;
                }
            };
            return operation;
        }

        /// <summary>
        /// Method that unloads some scene based on the "sceneName" parameter.
        /// </summary>
        /// <param name="sceneName">Defines the scene name.</param>
        public static AsyncOperation Unload(string sceneName)
            => SceneManager.UnloadSceneAsync(sceneName);

        #endregion

        #region Private methods

        #region Lifecycle

        private void Awake()
        {
            if (_instance)
            {
                DestroyImmediate(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
            _scenes.Push(SceneManager.GetActiveScene().name);
        }

        #endregion

        #endregion

        #region Private static methods

        /// <summary>
        /// Method that organize the game canvas.
        /// </summary>
        private static void SortCanvas(AsyncOperation _)
        {
            var canvas = FindObjectsOfType<Canvas>();
            for (var i = 0; i < canvas.Length; i++)
            {
                canvas[i].sortingOrder = i;
            }
        }

        #endregion

        private static NavigationController _instance;
        private readonly Stack<string> _scenes = new();
    }
}
