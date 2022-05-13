using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project._UI.Scripts
{
    [RequireComponent(typeof(Button))]
    public class ButtonUI : MonoBehaviour
    {
        #region Public properties

        public bool Interactable
        {
            get => _button.interactable;
            set => _button.interactable = value;
        }

        #endregion

        #region Public events

        public event UnityAction Click
        {
            add => _button.onClick.AddListener(value);
            remove => _button.onClick.RemoveListener(value);
        }

        #endregion

        #region Private methods

        #region Lifecycle

        private void Awake() => _button = GetComponent<Button>();

        #endregion

        #endregion

        private Button _button;
    }
}
