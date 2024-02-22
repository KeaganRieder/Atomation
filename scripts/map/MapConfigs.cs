using Godot;
using Newtonsoft.Json;
using Atomation.Resources;
using Newtonsoft.Json.Converters;

namespace Atomation.Map
{
    /// <summary>
    /// config data used for setting how noise maps are generated
    /// </summary>
    public class NoiseMapConfig
    {
        [JsonProperty(Order = 1)]
        public int seed;
        [JsonProperty(Order = 2)]
        public int octaves;
        
        [JsonProperty(Order = 3)]
        public float zoom;
        [JsonProperty(Order = 4)]
        public float frequency;
        [JsonProperty(Order = 5)]
        public float persistence;
        [JsonProperty(Order = 5)]
        public float lacunarity;

        /// <summary>
        /// the distance a point is from the center position
        /// </summary>
        [JsonIgnore]
        public Vector2 offset;

        /// <summary>
        /// decides how octaves get combined in the generation of the noise map
        /// </summary>
        [JsonProperty(Order = 9)]
        public FastNoiseLite.FractalTypeEnum fractalType;

        /// <summary>
        /// decides noise type
        /// </summary>
        [JsonProperty(Order = 10)]
        public FastNoiseLite.NoiseTypeEnum noiseType;

    }

    /// <summary>
    /// class which is used to pass a collection of data that relates to the 
    /// configuration of world world generation.
    /// this data is passed between the many world generation classes
    /// </summary>
    public class GenConfigs
    {
        //general configs
        /// <summary>
        /// bounds/max size of the world
        /// x = width
        /// y = height
        /// </summary>
        [JsonProperty(Order = 1)]
        public Vector2I worldBounds;

        //
        // generation configs   
        //

        //terrain configs
        [JsonProperty(Order = 2)]
        public float seaLevel = .2f;
        [JsonProperty(Order = 3)]
        public float mountainSize = .8f; 

        // noise maps
        [JsonProperty(Order = 4)]
        public NoiseMapConfig elevationMapConfigs;
        [JsonProperty(Order = 5)]
        public NoiseMapConfig moistureMapConfigs;
        [JsonProperty(Order = 6)]
        public NoiseMapConfig heatMapConfigs;

        public void FormatConfig(string path, string fileName){
            Resources.JsonWriter.WriteFile(path, fileName, this);
        }
    }
}