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
        public float moisture;
        [JsonProperty("avg temperature", Order = 4)]
        public float temperature;

        [JsonProperty("biome terrain", Order = 5)]
        public Dictionary<float, string> biomeTerrain;

        [JsonProperty("biome color", Order = 6)]
        public Color color;

        public BiomeDef(string name, float moisture, float maxMoisture, float temperature, float maxTemperature,
                Dictionary<float, string> biomeTerrain, Color color)
        {
            this.Name = name;
            this.moisture = moisture;
            this.temperature = temperature;
            this.biomeTerrain = biomeTerrain;
            this.color = color;
        }

        [JsonIgnore]
        public override string Label
        {
            get
            {
                return JsonConvert.SerializeObject(new Vector2(temperature,moisture));
            }
        }

    }
}