using System.Collections.Generic;
using UnityEngine;

namespace _Project.Map.Scripts
{
    /// <summary>
    /// Scriptable object responsible for handling any data related with the map factory business logic.
    /// </summary>
    [CreateAssetMenu(menuName = "Map/Settings", fileName = "new Map Settings")]
    internal class SettingsSO : ScriptableObject
    {
        #region Internal properties

        /// <summary>
        /// Property that says if the map must be created when the scene starts.
        /// </summary>
        internal bool GenerateOnStart => generateOnStart;

        /// <summary>
        /// Property that says if the map must be updated each frame.
        /// </summary>
        internal bool Update => update;

        /// <summary>
        /// Property that defines the map size.
        /// Obs: It must be multiple a power of 2.
        /// </summary>
        internal uint Size => size;

        /// <summary>
        /// Property that defines the grid octaves.
        /// </summary>
        internal uint Octaves => octaves;

        /// <summary>
        /// Property that defines the relief height.
        /// </summary>
        internal float Depth => depth;

        /// <summary>
        /// Property that defines the noise scale.
        /// </summary>
        internal float NoiseScale => noiseScale;

        /// <summary>
        /// Property that defines the noise persistance.
        /// </summary>
        internal float Persistance => persistance;

        /// <summary>
        /// Property that defines the noise lacunarity.
        /// </summary>
        internal float Lacunarity => lacunarity;

        /// <summary>
        /// Property that defines the noiseOffset.
        /// </summary>
        internal Vector2 NoiseOffset => noiseOffset;

        /// <summary>
        /// Property that defines the map seed.
        /// </summary>
        internal string Seed => seed;

        /// <summary>
        /// Property that defines the relief height curve.
        /// </summary>
        internal AnimationCurve HeightCurve => heightCurve;

        /// <summary>
        /// Property that defines the map draw mode.
        /// </summary>
        internal DrawMode Mode => mode;

        /// <summary>
        /// Property that defines an array that contains all the map regions.
        /// </summary>
        internal IEnumerable<Region> Regions => regions;

        #endregion

        [SerializeField] private bool generateOnStart;
        [SerializeField] private bool update = true;

        [Space, Min(1), SerializeField] private uint size = 512;
        [Min(1), SerializeField] private uint octaves = 16;
        [SerializeField] private float depth;
        [Range(0f, 1f), SerializeField] private float persistance = .5f;
        [Min(1), SerializeField] private float lacunarity = 2f;
        [Min(0.01f), SerializeField] private float noiseScale = 50f;

        [Space, SerializeField] private Vector2 noiseOffset;

        [Space, SerializeField] private string seed;

        [Space, SerializeField] private AnimationCurve heightCurve;

        [Space, SerializeField] private DrawMode mode;
        [SerializeField] private Region[] regions;
    }
}
