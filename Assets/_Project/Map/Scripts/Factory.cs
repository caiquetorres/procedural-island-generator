using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Map.Scripts
{
    internal class Factory : MonoBehaviour
    {
        #region Internal methods

        internal void Create()
        {
            GenerateBaseGrid();
            Clean();

            for (var i = 0; i < factorySettings.Iterations; i++)
            {
                Iterate();
            }

            InstantiateObjects();
        }

        #endregion

        #region Private methods

        #region Lifecycle

        private void Awake() => Random.InitState(factorySettings.Seed.GetHashCode());

        #endregion

        private void Clean()
        {
            if (_blocks != null)
            {
                foreach (var block in _blocks)
                {
                    Destroy(block.gameObject);
                }
            }

            _blocks = new Block[factorySettings.Size, factorySettings.Size];
        }

        private void GenerateBaseGrid()
        {
            _islandMap = new int[factorySettings.Size, factorySettings.Size];

            for (var i = 0; i < factorySettings.Size; i++)
            {
                for (var j = 0; j < factorySettings.Size; j++)
                {
                    _islandMap[i, j] = 0;
                }
            }

            for (var col = factorySettings.DistanceFromBorders;
                 col < factorySettings.Size - factorySettings.DistanceFromBorders;
                 col++)
            {
                for (var row = factorySettings.DistanceFromBorders;
                     row < factorySettings.Size - factorySettings.DistanceFromBorders;
                     row++)
                {
                    _islandMap[col, row] = Random.value < factorySettings.Density ? 60 : 10;
                }
            }
        }

        private void Iterate()
        {
            var map = new int[factorySettings.Size, factorySettings.Size];

            for (var i = 0; i < factorySettings.Size; i++)
            {
                for (var j = 0; j < factorySettings.Size; j++)
                {
                    map[i, j] = 0;
                }
            }

            for (var col = 1; col < factorySettings.Size - 1; col++)
            {
                for (var row = 1; row < factorySettings.Size - 1; row++)
                {
                    var neighbours = 0;
                    var self = _islandMap[col, row];

                    for (var i = -1; i <= 1; i++)
                    {
                        for (var j = -1; j <= 1; j++)
                        {
                            neighbours += _islandMap[col + i, row + j];
                        }
                    }

                    neighbours -= self;
                    neighbours /= 8;

                    if (neighbours < 40)
                    {
                        map[col, row] = self + Random.Range(0, neighbours - self);
                    }
                    else if (self > neighbours)
                    {
                        map[col, row] = self + Random.Range(-10, 10);
                    }
                    else
                    {
                        map[col, row] = self + Random.Range(0, neighbours / 5);
                    }
                }
            }

            _islandMap = map;
        }

        private void InstantiateObjects()
        {
            for (var col = 0; col < factorySettings.Size; col++)
            {
                for (var row = 0; row < factorySettings.Size; row++)
                {
                    var instance = Instantiate(blockPrefab, parentTransform);
                    var instanceTransform = instance.transform;

                    instanceTransform.localScale = Vector3.one;
                    instanceTransform.position = new Vector3(
                        col - factorySettings.Size / 2,
                        0,
                        row - factorySettings.Size / 2);

                    var value = _islandMap[col, row];
                    instance.Initialize(value, sand, grass);

                    _blocks[col, row] = instance;
                }
            }
        }

        #endregion

        private int[,] _islandMap;

        private Block[,] _blocks;

        [SerializeField] private int sand;
        [SerializeField] private int grass;

        [Space, SerializeField] private FactorySettingsSO factorySettings;

        [Space, SerializeField] private Block blockPrefab;

        [Space, SerializeField] private Transform parentTransform;
    }
}
