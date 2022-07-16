using System;
using UnityEngine;
using Random = System.Random;

namespace _Project.Map.Scripts
{
    /// <summary>
    /// Struct that represents a noise map.
    /// </summary>
    internal readonly struct NoiseMap
    {
        #region Internal properties

        /// <summary>
        /// Property that contains all the noise heights.
        /// </summary>
        /// <param name="x">Defines the x axis value.</param>
        /// <param name="y">Defines the y axis value.</param>
        internal float this[uint x, uint y] => _map[x, y];

        #endregion

        #region Internal contructors

        internal NoiseMap(
            uint size,
            int seed,
            float scale,
            uint octaves,
            float persistence,
            float lacunarity,
            Vector2 offset)
        {
            _size = size;
            _seed = seed;
            _scale = scale;
            _octaves = octaves;
            _persistence = persistence;
            _lacunarity = lacunarity;
            _offset = offset;
            _map = new float[size, size];

            Create();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method that creates a new noise map.
        /// </summary>
        private void Create()
        {
            if (_scale <= 0)
            {
                throw new Exception("The scale must be greater than zero");
            }

            var generator = new Random(_seed);
            var octavesOffsets = new Vector2[_octaves];

            for (var i = 0; i < _octaves; i++)
            {
                var offsetX = generator.Next(-10000, 10000) + _offset.x;
                var offsetY = generator.Next(-10000, 10000) + _offset.y;

                octavesOffsets[i] = new Vector2(offsetX, offsetY);
            }

            var maxNoiseHeight = float.MinValue;
            var minNoiseHeight = float.MaxValue;

            var halfWidth = _size / 2;
            var halfHeight = _size / 2;

            for (var j = 0; j < _size; j++)
            {
                for (var i = 0; i < _size; i++)
                {
                    var amplitude = 1f;
                    var frequency = 1f;
                    var noiseHeight = 0f;

                    for (var k = 0; k < _octaves; k++)
                    {
                        var x = (i - halfWidth) / _scale * frequency + octavesOffsets[k].x;
                        var y = (j - halfHeight) / _scale * frequency + octavesOffsets[k].y;
                        var perlinValue = Mathf.PerlinNoise(x, y) * 2 - 1;

                        noiseHeight += perlinValue * amplitude;
                        amplitude *= _persistence;
                        frequency *= _lacunarity;
                    }

                    if (noiseHeight > maxNoiseHeight)
                    {
                        maxNoiseHeight = noiseHeight;
                    }

                    if (noiseHeight < minNoiseHeight)
                    {
                        minNoiseHeight = noiseHeight;
                    }

                    _map[i, j] = noiseHeight;
                }
            }

            for (var j = 0; j < _size; j++)
            {
                for (var i = 0; i < _size; i++)
                {
                    _map[i, j] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, _map[i, j]);
                }
            }
        }

        #endregion

        private readonly uint _size;
        private readonly uint _octaves;
        private readonly int _seed;
        private readonly float _scale;
        private readonly float _persistence;
        private readonly float _lacunarity;

        private readonly Vector2 _offset;

        private readonly float[,] _map;
    }
}
