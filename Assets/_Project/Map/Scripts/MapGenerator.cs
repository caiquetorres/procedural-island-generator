using UnityEngine;

namespace _Project.Map.Scripts
{
    [ExecuteAlways]
    internal class MapGenerator : MonoBehaviour
    {
        internal void GenerateMap()
        {
            var noiseMap = Noise.GenerateNoiseMap(
                size,
                seed.GetHashCode(),
                noiseScale,
                octaves,
                persistance,
                lacunarity,
                offset);

            if (asIsland)
            {
                var falloffMap = Falloff.GenerateFalloffMap(size);
                for (var y = 0; y < size; y++)
                {
                    for (var x = 0; x < size; x++)
                    {
                        noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                    }
                }
            }

            mapDisplay.Draw(noiseMap);
        }

        private void Update() => GenerateMap();

        [SerializeField] private string seed;

        [Space, SerializeField] private bool asIsland;
        [Min(1), SerializeField] private uint size = 100;
        [Min(1), SerializeField] private uint octaves;
        [Min(0.01f), SerializeField] private float noiseScale;
        [Range(0f, 1f), SerializeField] private float persistance;
        [Min(1), SerializeField] private float lacunarity;

        [Space, SerializeField] private Vector2 offset;

        [Space, SerializeField] private MapDisplay mapDisplay;
    }
}
