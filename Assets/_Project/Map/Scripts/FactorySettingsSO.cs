using UnityEngine;

namespace _Project.Map.Scripts
{
    /// <summary>
    /// Scriptable object that handles all the factory settings.
    /// </summary>
    [CreateAssetMenu(menuName = "Map/Factory/Settings", fileName = "new Map Factory Settings")]
    internal class FactorySettingsSO : ScriptableObject
    {
        #region Internal properties

        /// <summary>
        /// Property that defines the island distance from the borders.
        /// </summary>
        internal uint DistanceFromBorders => distanceFromBorders;

        /// <summary>
        /// Property that defines the interactions amount.
        /// </summary>
        internal uint Iterations => iterations;

        /// <summary>
        /// Property that defines the map size.
        /// </summary>
        internal uint Size => size;

        /// <summary>
        /// Property that defines the island density.
        /// </summary>
        internal float Density => density / 100f;

        /// <summary>
        /// Property that defines the map seed.
        /// </summary>
        internal string Seed
        {
            get => seed;
            set => seed = value;
        }

        #endregion

        [SerializeField] private string seed = "default";
        [Min(1), SerializeField] private byte iterations = 5;
        [Min(1), SerializeField] private uint distanceFromBorders = 2;
        [SerializeField] private uint size = 100;
        [Range(0f, 100f), SerializeField] private byte density = 45;
    }
}
