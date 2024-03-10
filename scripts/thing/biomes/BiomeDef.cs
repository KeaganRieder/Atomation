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

        /// <summary>
        /// used to cerate the label for the biome def. which will be used as 
        /// the key to mark it's storage upon catching done in "defDatabase"
        /// </summary>
        public struct BiomeLabel
        {
            public float minMoisture;
            public float maxMoisture;
            public float minTemperature;
            public float maxTemperature;
            
        }

        [JsonIgnore]
        public override string Label
        {
            get
            {
                BiomeLabel biomeLabel = new BiomeLabel(){
                    minMoisture = minMoisture,
                    maxMoisture = maxMoisture,
                    minTemperature = minTemperature,
                    maxTemperature = maxTemperature,
                };
                return JsonConvert.SerializeObject(biomeLabel);
            }
        }

    }
}