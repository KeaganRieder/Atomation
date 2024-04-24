using Atomation.Resources;
using Godot;
using Newtonsoft.Json;

namespace Atomation.Map;

/// <summary>
/// class which stores the settings used to configure the map
/// in order to create unique play through and such
/// </summary>
public class MapSettings
{
	/// <summary>
	/// how many tiles away are render at any given point
	/// </summary
	[JsonProperty("Render Distance", Order = 1)]
	public static float MaxLoadDistance = 32; //64

	[JsonProperty("Gen Settings", Order = 2)]
	public GenSettings GenSettings;

	public MapSettings()
	{
		GenSettings = new GenSettings();
	}
}

/// <summary>
/// Settings which allow configuring generation of the map
/// </summary>
public class GenSettings
{
	/// <summary>
	/// center of the world often the spawn point and normally at 0,0
	/// </summary>
	[JsonProperty("Center", Order = 1)]
	public Vector2 Center;

	/// <summary>
	/// the size of the world default is TO BE determined
	/// </summary>
	[JsonProperty("World Size", Order = 2)]
	public Vector2I WorldSize;

	/// <summary>
	/// the seed of the world used fro random generation
	/// </summary>
	[JsonProperty("World Seed", Order = 3)]
	public int Seed;

	/// <summary>
	/// scale or zoom level of world
	/// </summary>
	[JsonProperty("Zoom", Order = 4)]
	public float Scale;

	/// <summary>
	/// configs for height map generation 
	/// </summary>
	[JsonProperty("Height Configs", Order = 6)]
	public HeightMapConfigs HeightMapConfigs;

	/// <summary>
	/// configs for temperature map generation 
	/// </summary>
	[JsonProperty("Temperature Configs", Order = 7)]
	public TemperatureMapConfigs TemperatureMapConfigs;

	/// <summary>
	/// configs for Moisture map generation 
	/// </summary>
	[JsonProperty("Moisture map Configs", Order = 8)]
	public MoistureMapConfigs MoistureMapConfigs;

	public GenSettings()
	{
		HeightMapConfigs = new HeightMapConfigs();
		TemperatureMapConfigs = new TemperatureMapConfigs();
		MoistureMapConfigs = new MoistureMapConfigs();
	}
}