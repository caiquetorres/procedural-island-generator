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

            var width = noiseMap.GetLength(0);
            var height = noiseMap.GetLength(1);

            var texture = mode switch
            {
                DrawMode.Noise => TextureGenerator.TextureFromHeightMap(noiseMap),
                _ => TextureGenerator.TextureFromColorMap(CreateColorMap(noiseMap), width, height)
            };

            terrain.materialTemplate.mainTexture = texture;
            terrain.transform.localScale = new Vector3(texture.width, 1, texture.height);
        }

        #endregion

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

        [SerializeField] private DrawMode mode;

        [Space, SerializeField] private Terrain terrain;

        [Space, SerializeField] private Region[] regions;
    }
}
