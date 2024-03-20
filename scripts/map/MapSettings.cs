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
		public const int CELL_SIZE = 8;

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
		
		public MapGenSettings(){
			heightMapConfigs = new HeightMapConfigs();
			temperatureMapConfigs = new TemperatureMapConfigs();
			moistureMapConfigs = new MoistureMapConfigs();
		}
	}

}
