using System;
using UnityEngine;

namespace _Project.Map.Scripts
{
    internal class Block : MonoBehaviour
    {
        #region Internal methods

        internal void Initialize(int value, int sand, int grass)
        {
            Color color;
            if (value > 150)
            {
                color = green;
            }
            else if (value > grass)
            {
                color = Color.green;
            }
            else if (value > sand)
            {
                color = Color.yellow;
            }
            else
            {
                color = Color.blue;
            }

            _meshRenderer.material.SetColor(BaseColor, color);
        }

        #endregion

        #region Private methods

        #region Lifecycle

        private void Awake() => _meshRenderer = GetComponent<Renderer>();

        #endregion

        #endregion


        [SerializeField] private Color green;
        private Renderer _meshRenderer;
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    }
}
