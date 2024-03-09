using Atomation.Resources;
using Godot;
using Newtonsoft.Json;

namespace Atomation.Map
{
	/// <summary>
	/// class which stores the settings used to configure the map
	/// in order to create unique playthroughs and such
	/// </summary>
	public class MapSettings
	{
		[JsonIgnore]
		public const float CELL_SIZE = 16;
		[JsonProperty("render distance", Order = 1)]
		public static float MAX_LOAD_DIST = 64;

		[JsonProperty("generation settings", Order = 2)]
		public MapGenSettings genSettings;

		//todo when working on Saving

		public MapSettings(){
			genSettings = new MapGenSettings();
		}
	}

	/// <summary>
	/// Settings which allow configuring generation of the map
	/// </summary>
	public class MapGenSettings
	{
		[JsonProperty("world size", Order = 1)]
		public Vector2I worldSize;

		[JsonProperty("world seed", Order = 2)]
		public int seed;
		[JsonProperty("map zoom", Order = 3)]
		public float scale; 

		[JsonProperty(Order = 4)]
		public float seaLevel = .2f;
		[JsonProperty(Order = 5)]
		public float mountainSize = .8f; 

		[JsonProperty("height map configs", Order = 6)]
		public HeightMapConfigs heightMapConfigs;
		[JsonProperty("temperature map configs", Order = 7)]
		public TemperatureMapConfigs temperatureMapConfigs;
		[JsonProperty(" moisture map configs", Order = 8)]
		public MoistureMapConfigs moistureMapConfigs;

		//old values

		// [JsonProperty("elevation settings", Order = 6)]
		// public NoiseMapSettings elevationSettings;

		// [JsonProperty("temperature settings", Order = 7)]
		// public NoiseMapSettings temperatureSettings;

		// [JsonProperty("moisture settings", Order = 8)]
		// public NoiseMapSettings moistureSettings;

		public MapGenSettings(){
			heightMapConfigs = new HeightMapConfigs();
			temperatureMapConfigs = new TemperatureMapConfigs();
			moistureMapConfigs = new MoistureMapConfigs();
		}
	}

	// /// <summary>
	// /// settings which handle configuring noise Maps used during world generation
	// /// </summary>
	// public class NoiseMapSettings
	// {
	// 	[JsonProperty(Order = 1)]
	// 	/// <summary>
	// 	/// how many noise layers are applied to this map
	// 	/// </summary>
	// 	public int octaves;
	
	// 	[JsonProperty(Order = 3)]
	// 	/// <summary>
	// 	/// the frequency of the noise
	// 	/// </summary>
	// 	public float frequency;
	// 	[JsonProperty(Order = 4)]
	// 	/// <summary>
	// 	/// the gain of the noise
	// 	/// </summary>
	// 	public float persistence;
	// 	[JsonProperty(Order = 5)]
	// 	public float lacunarity;

	// 	/// <summary>
	// 	/// the distance a point is from the center position
	// 	/// </summary>
	// 	[JsonIgnore]
	// 	public Vector2 offset;

	// 	/// <summary>
	// 	/// noise type
	// 	/// </summary>
	// 	[JsonProperty(Order = 10)]
	// 	public FastNoiseLite.NoiseTypeEnum noiseType;

	// 	/// <summary>
	// 	/// decides how octaves get combined in the generation of the noise map
	// 	/// </summary>
	// 	[JsonProperty(Order = 9)]
	// 	public FastNoiseLite.FractalTypeEnum fractalType;

	  
	// }
}
