using System;
using UnityEngine;

namespace _Project.Map.Scripts
{
    [Serializable]
    internal struct Region
    {
        #region Internal properties

        internal float Height => height;

        internal Color Color => color;

        #endregion

        [SerializeField] private string name;
        [SerializeField] private float height;
        [SerializeField] private Color color;
    }
}
