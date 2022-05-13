using UnityEngine;

namespace _Project.Map.Scripts
{
    internal static class Falloff
    {
        internal static float[,] GenerateFalloffMap(uint size)
        {
            var map = new float[size, size];

            for (var j = 0; j < size; j++)
            {
                for (var i = 0; i < size; i++)
                {
                    var x = (float) i / size * 2 - 1;
                    var y = (float) j / size * 2 - 1;

                    map[i, j] = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                }
            }

            return map;
        }
    }
}
