using UnityEngine;

namespace _Project.Map.Scripts
{
    internal static class Falloff
    {
        internal static float[,] GenerateFalloffMap(uint width, uint height)
        {
            var map = new float[width, height];

            for (var j = 0; j < height; j++)
            {
                for (var i = 0; i < width; i++)
                {
                    var x = (float) i / width * 2 - 1;
                    var y = (float) j / height * 2 - 1;

                    map[i, j] = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                }
            }

            return map;
        }
    }
}
