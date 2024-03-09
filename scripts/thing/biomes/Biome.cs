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
        private float moisture;
        private float temperature;

        private Dictionary<float, string> biomeTerrain;

        private Color color;

        public Biome(BiomeDef configs){
            name = configs.Name;
            moisture = configs.moisture;
            temperature = configs.temperature;
            biomeTerrain = configs.biomeTerrain;
            color= configs.color;
        }

        public Color Color{get => color;}

        /// <summary>
        /// given temperature and moisture checks to see if they are
        /// within the min and max range for the biomes requirements
        /// </summary>
        // public bool Suitable(float temperature, float moisture)
        // {
        //     // bool suitableMoisture = moisture >= minMoisture && moisture <= maxMoisture;
        //     // bool suitableTemperature = temperature >= minTemperature && temperature <= maxTemperature;
        //     // if (suitableMoisture && suitableTemperature)
        //     // {
        //     //     return true;
        //     // }
        //     // else
        //     // {
        //     //     return false;
        //     // }
        // }

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