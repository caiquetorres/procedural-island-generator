using UnityEngine;

namespace _Project.Map.Scripts
{
    internal class MapDisplay : MonoBehaviour
    {
        #region Internal methods

        internal void Draw(float[,] noiseMap)
        {
            if (!terrain)
            {
                return;
            }

            var size = noiseMap.GetLength(0);

            var texture = mode switch
            {
                DrawMode.Noise => TextureGenerator.TextureFromHeightMap(noiseMap),
                _ => TextureGenerator.TextureFromColorMap(CreateColorMap(noiseMap), size)
            };

            terrain.materialTemplate.mainTexture = texture;

            ApplyTerrain(noiseMap);
        }

        #endregion

        private void ApplyTerrain(float[,] heightMap)
        {
            var size = heightMap.GetLength(0);
            var terrainData = terrain.terrainData;

            var map = new float[size, size];

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    map[size - 1 - y, size - 1 - x] = heightCurve.Evaluate(heightMap[size - 1 - x, size - 1 - y]);
                }
            }

            terrain.transform.localScale = new Vector3(size, 1, size);

            terrainData.heightmapResolution = size;
            terrainData.size = new Vector3(size, depth, size);
            terrainData.SetHeights(0, 0, map);
            terrain.Flush();
        }

        #region Private methods

        private Color[] CreateColorMap(float[,] noiseMap)
        {
            var width = noiseMap.GetLength(0);
            var height = noiseMap.GetLength(1);

            var colorMap = new Color[width * height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var currentHeight = noiseMap[x, y];
                    foreach (var region in regions)
                    {
                        if (currentHeight > region.Height)
                        {
                            continue;
                        }

                        colorMap[y * width + x] = region.Color;

                        break;
                    }
                }
            }

            return colorMap;
        }

        #endregion

        [SerializeField] private float depth;
        [SerializeField] private DrawMode mode;
        [SerializeField] private AnimationCurve heightCurve;

        [Space, SerializeField] private Terrain terrain;

        [Space, SerializeField] private Region[] regions;
    }
}
