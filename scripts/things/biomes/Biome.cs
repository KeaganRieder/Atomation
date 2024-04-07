using System.Collections.Generic;
using Atomation.Resources;
using Godot;
using Newtonsoft.Json;

namespace Atomation.Thing
{
        public struct BiomeLabel
        {
            public float minMoisture;
            public float maxMoisture;
            public float minTemperature;
            public float maxTemperature;
            
        }

    /// <summary>
    /// biomes are used to determine the types of terrain based on
    /// the provide elevation, moisture and temperature
    /// </summary>
    public class Biome : Thing
    {
        [JsonProperty("minMoisture")]
        public float MinMoisture;
        [JsonProperty("maxMoisture")]
        public float MaxMoisture;
        [JsonProperty("minTemperature")]
        public float MinTemperature;
        [JsonProperty("maxTemperature")]
        public float MaxTemperature;

        [JsonProperty("biomeTerrain")]
        private Dictionary<float, string> terrain;

        [JsonProperty("biomeColor")]
        public Color Color;

        [JsonIgnore]
        public override string Key
        {
            get
            {
                BiomeLabel biomeLabel = new BiomeLabel(){
                    minMoisture = MinMoisture,
                    maxMoisture = MaxMoisture,
                    minTemperature = MinTemperature,
                    maxTemperature = MaxTemperature,
                };
                return JsonConvert.SerializeObject(biomeLabel);
            }
        }

        public Biome()
        {
        }



        /// <summary>
        /// returns the key for the terrain which is present in the biome
        /// and corresponds to the
        /// </summary>
        public TerrainDef GetTerrain(float elevation)
        {
            //try and get a terrain
            foreach (float terrainHeight in terrain.Keys)
            {
                if (elevation < terrainHeight)
                {
                    return DefDatabase.GetTerrainConfig(terrain[terrainHeight]);
                }
            }

            // if no terrain meeting requirement then pick closest
            return null;
        }


    }
}