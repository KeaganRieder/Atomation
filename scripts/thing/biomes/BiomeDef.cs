using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

namespace Atomation.Thing
{
    /// <summary>
    /// config file used for all BiomeDef in the game
    /// </summary>
    public class BiomeDef : ThingDef
    {
        [JsonProperty("avg moisture", Order = 3)]
        public float minMoisture;
        [JsonProperty("avg temperature", Order = 4)]
        public float minTemperature;

        [JsonProperty("biome terrain", Order = 5)]
        public Dictionary<float, string> biomeTerrain;

        [JsonProperty("biome color", Order = 6)]
        public Color color;

        public BiomeDef(string name,float minMoisture, float maxMoisture, float minTemperature, float maxTemperature,
                Dictionary<float, string> biomeTerrain, Color color)
        {
            this.Name = name;
            this.minMoisture = minMoisture;
            this.minTemperature = minTemperature;
            this.biomeTerrain = biomeTerrain;
            this.color= color;
        }

        /// <summary>
        /// given temperature and moisture checks to see if they are
        /// within the min and max range for the biomes requirements
        /// </summary>
        public bool Suitable(float temperature, float moisture)
        {
            // bool suitableMoisture = moisture >= minMoisture && moisture <= maxMoisture;
            // bool suitableTemperature = temperature >= minTemperature && temperature <= maxTemperature;
            // if (suitableMoisture && suitableTemperature)
            // {
            //     return true;
            // }
            // else
            // {
                return false;
            // }
        }

    }
}