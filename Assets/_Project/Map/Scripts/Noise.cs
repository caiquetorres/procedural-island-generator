using System;
using UnityEngine;
using Random = System.Random;

namespace _Project.Map.Scripts
{
    internal static class Noise
    {
        internal static float[,] GenerateNoiseMap(
            uint size,
            int seed,
            float scale, uint octaves,
            float persistance,
            float lacunarity,
            Vector2 offset)
        {
            if (scale <= 0)
            {
                throw new Exception("The scale must be greater than zero");
            }

            var generator = new Random(seed);
            var octavesOffsets = new Vector2[octaves];

            for (var i = 0; i < octaves; i++)
            {
                var offsetX = generator.Next(-10000, 10000) + offset.x;
                var offsetY = generator.Next(-10000, 10000) + offset.y;

                octavesOffsets[i] = new Vector2(offsetX, offsetY);
            }

            var map = new float[size, size];

            var maxNoiseHeight = float.MinValue;
            var minNoiseHeight = float.MaxValue;

            var halfWidth = size / 2;
            var halfHeight = size / 2;

            for (var j = 0; j < size; j++)
            {
                for (var i = 0; i < size; i++)
                {
                    var amplitude = 1f;
                    var frequency = 1f;
                    var noiseHeight = 0f;

                    for (var k = 0; k < octaves; k++)
                    {
                        var x = (i - halfWidth) / scale * frequency + octavesOffsets[k].x;
                        var y = (j - halfHeight) / scale * frequency + octavesOffsets[k].y;
                        var perlinValue = Mathf.PerlinNoise(x, y) * 2 - 1;

                        noiseHeight += perlinValue * amplitude;
                        amplitude *= persistance;
                        frequency *= lacunarity;
                    }

                    if (noiseHeight > maxNoiseHeight)
                    {
                        maxNoiseHeight = noiseHeight;
                    }

                    if (noiseHeight < minNoiseHeight)
                    {
                        minNoiseHeight = noiseHeight;
                    }

                    map[i, j] = noiseHeight;
                }
            }

            for (var j = 0; j < size; j++)
            {
                for (var i = 0; i < size; i++)
                {
                    map[i, j] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, map[i, j]);
                }
            }

            return map;
        }
    }
}
