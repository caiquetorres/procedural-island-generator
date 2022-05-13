using UnityEngine;

namespace _Project.Map.Scripts
{
    internal static class TextureGenerator
    {
        #region Internal static methods

        internal static Texture2D TextureFromColorMap(Color[] colorMap, int size)
        {
            var texture = new Texture2D(size, size)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };

            texture.SetPixels(colorMap);
            texture.Apply();

            return texture;
        }

        internal static Texture2D TextureFromHeightMap(float[,] heightMap)
        {
            var size = heightMap.GetLength(0);

            var colorMap = new Color[size * size];

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    colorMap[y * size + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
                }
            }

            return TextureFromColorMap(colorMap, size);
        }

        #endregion
    }
}
