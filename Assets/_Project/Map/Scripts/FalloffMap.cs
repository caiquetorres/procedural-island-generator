using UnityEngine;

namespace _Project.Map.Scripts
{
    /// <summary>
    /// Struct that represents a falloff map.
    /// </summary>
    internal readonly struct FalloffMap
    {
        #region Internal properties

        /// <summary>
        /// Property that contains all the falloff heights.
        /// </summary>
        /// <param name="x">Defines the x axis value.</param>
        /// <param name="y">Defines the y axis value.</param>
        internal float this[uint x, uint y] => _map[x, y];

        #endregion

        #region Internal contructors

        internal FalloffMap(uint size)
        {
            _size = size;
            _map = new float[size, size];

            Create();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method that creates a new falloff map.
        /// </summary>
        private void Create()
        {
            for (var j = 0; j < _size; j++)
            {
                for (var i = 0; i < _size; i++)
                {
                    var x = (float) i / _size * 2 - 1;
                    var y = (float) j / _size * 2 - 1;

                    _map[i, j] = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                }
            }
        }

        #endregion

        private readonly uint _size;

        private readonly float[,] _map;
    }
}
