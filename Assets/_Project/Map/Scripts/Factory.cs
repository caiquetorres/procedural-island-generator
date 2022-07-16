using UnityEngine;

namespace _Project.Map.Scripts
{
    /// <summary>
    /// Component responsible for dealing with the map factory.
    /// </summary>
    [ExecuteAlways]
    internal class Factory : MonoBehaviour
    {
        #region Private methods

        #region Lifecycle

        private void Start()
        {
            if (settings != null && settings.GenerateOnStart)
            {
                Generate();
            }
        }

        private void Update()
        {
            if (settings != null && settings.Update)
            {
                Generate();
            }
        }

        #endregion

        /// <summary>
        /// Method that creates a new map based on the settings data.
        /// </summary>
        private void Generate()
        {
            var size = settings.Size;

            var falloffMap = new FalloffMap(size);
            var noiseMap = new NoiseMap(
                size,
                settings.Seed.GetHashCode(),
                settings.NoiseScale,
                settings.Octaves,
                settings.Persistance,
                settings.Lacunarity,
                settings.NoiseOffset);

            _heightMap = new float[size, size];

            for (uint y = 0; y < settings.Size; y++)
            {
                for (uint x = 0; x < settings.Size; x++)
                {
                    _heightMap[y, x] = Mathf.Clamp01(noiseMap[y, x] - falloffMap[y, x]);
                }
            }

            Draw();
        }

        /// <summary>
        /// Method that, given a float matrix, changes the terrain mesh.
        /// </summary>
        private void Draw()
        {
            if (!terrain)
            {
                return;
            }

            var size = _heightMap.GetLength(0);
            var terrainData = terrain.terrainData;

            terrain.materialTemplate.mainTexture = settings.Mode.Equals(DrawMode.Color)
                ? GetColorTextureFromHeightMap(_heightMap)
                : GetTextureFromHeightMap(_heightMap);

            for (uint y = 0; y < settings.Size; y++)
            {
                for (uint x = 0; x < settings.Size; x++)
                {
                    _heightMap[y, x] = settings.HeightCurve.Evaluate(_heightMap[y, x]);
                }
            }

            terrainData.heightmapResolution = size;
            terrainData.size = new Vector3(size, settings.Depth, size);
            terrainData.SetHeights(0, 0, _heightMap);

            terrain.Flush();
        }

        /// <summary>
        /// Method that generates a texture with values between white and black, where white colors represents the
        /// biggest heights, and the black colors represents the lowest heights.
        /// </summary>
        /// <param name="heightMap">Defines a 2d array that contains all the terrain heights.</param>
        /// <returns>The generated texture.</returns>
        private Texture2D GetTextureFromHeightMap(float[,] heightMap)
        {
            var size = heightMap.GetLength(0);

            var colorMap = new Color[size * size];

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    colorMap[y * size + x] = Color.Lerp(Color.black, Color.white, heightMap[y, x]);
                }
            }

            var texture = new Texture2D(size, size)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };

            texture.SetPixels(colorMap);
            texture.Apply();

            return texture;
        }

        /// <summary>
        /// Method that generates a texture with all the colors represented by the regions array, present in settings.
        /// </summary>
        /// <param name="heightMap">Defines a 2d array that contains all the terrain heights.</param>
        /// <returns>The generated texture.</returns>
        private Texture2D GetColorTextureFromHeightMap(float[,] heightMap)
        {
            var size = heightMap.GetLength(0);
            var width = heightMap.GetLength(0);
            var height = heightMap.GetLength(1);

            var colorMap = new Color[width * height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var currentHeight = heightMap[y, x];
                    foreach (var region in settings.Regions)
                    {
                        if (currentHeight > region.MaxHeight)
                        {
                            continue;
                        }

                        colorMap[y * width + x] = region.Color;
                        break;
                    }
                }
            }

            var texture = new Texture2D(size, size)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };

            texture.SetPixels(colorMap);
            texture.Apply();

            return texture;
        }

        #endregion

        private float[,] _heightMap;

        [SerializeField] private SettingsSO settings;

        [Space, SerializeField] private Terrain terrain;
    }
}
