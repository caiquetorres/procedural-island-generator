using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Map.Scripts
{
    /// <summary>
    /// Struct that handles all the region data.
    /// <br />
    /// A region is the max height that can be used to assign colors to the generated map.
    /// </summary>
    [Serializable]
    internal struct Region
    {
        #region Internal properties

        /// <summary>
        /// Defines the max height.
        /// </summary>
        internal float MaxHeight => maxHeight;

        /// <summary>
        /// Defines the region color.
        /// </summary>
        internal Color Color => color;

        #endregion

        [SerializeField] private string name;
        [SerializeField] private float maxHeight;
        [SerializeField] private Color color;
    }
}
