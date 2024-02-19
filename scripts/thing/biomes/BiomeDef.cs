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
        [JsonProperty("min moisture", Order = 3)]
        public float minMoisture;
        [JsonProperty("max moisture", Order = 4)]
        public float maxMoisture;
        [JsonProperty("min temperature", Order = 5)]
        public float minTemperature;
        [JsonProperty("max temperature", Order = 6)]
        public float maxTemperature;

        [JsonProperty("biome terrain", Order = 7)]
        public Dictionary<float, string> biomeTerrain;

        [JsonProperty("biome color", Order = 8)]
        public Color color;

        public BiomeDef(string name,float minMoisture, float maxMoisture, float minTemperature, float maxTemperature,
                Dictionary<float, string> biomeTerrain, Color color)
        {
            this.Name = name;
            this.minMoisture = minMoisture;
            this.maxMoisture = maxMoisture;
            this.minTemperature = minTemperature;
            this.maxTemperature = maxTemperature;
            this.biomeTerrain = biomeTerrain;
            this.color= color;
        }

        /// <summary>
        /// given temperature and moisture checks to see if they are
        /// within the min and max range for the biomes requirements
        /// </summary>
        public bool Suitable(float temperature, float moisture)
        {
            bool suitableMoisture = moisture >= minMoisture && moisture <= maxMoisture;
            bool suitableTemperature = temperature >= minTemperature && temperature <= maxTemperature;
            if (suitableMoisture && suitableTemperature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}