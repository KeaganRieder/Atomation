using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

namespace Atomation.Thing
{
    /// <summary>
    /// biomes are used to determine the types of terrain based on
    /// the provide elevation, moisture and temperature
    /// </summary>
    public class Biome : Thing
    {
        private float minMoisture;
        private float maxMoisture;
        private float minTemperature;
        private float maxTemperature;

        private Dictionary<float, string> biomeTerrain;

        private Color color;

        public Biome(BiomeDef configs){
            name = configs.Name;
            minMoisture = configs.minMoisture;
            maxMoisture = configs.maxMoisture;
            minTemperature = configs.minTemperature;
            maxTemperature = configs.maxTemperature;

            biomeTerrain = configs.biomeTerrain;
            color= configs.color;
            color.A = 1;
        }

        public Color Color{get => color;}

        /// <summary>
        /// returns the key for the terrain which is present in the biome
        /// and corresponds to the
        /// </summary>
        public string GetTerrain(float elevation)
        {
            foreach (float terrainHeight in biomeTerrain.Keys)
            {
                if (elevation < terrainHeight)
                {
                    return biomeTerrain[terrainHeight];
                }
            }
            return null;
        }


    }
}