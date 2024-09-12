namespace Atomation.GameMap;

using Godot;
using Newtonsoft.Json;

/// <summary>
/// Contains the configs for the world. these configs are used to decide anything from
/// the size of the world to how the generators generate it
/// </summary>
public class MapConfigs
{
    /// <summary>
    /// the size of the world. by default this is set to a infante size however
    /// can be changed to a specified limit
    /// </summary>
    private Vector2I worldSize;

    private int seed;

    /// <summary>
    /// determines how zoomed in or out
    /// the map is
    /// </summary>
    private float mapScale;

    //
    // Terrain generation Configs
    //
    private float baseMoisture;
    /// <summary>
    /// highest temperature at the equator, used to offset temperature
    /// to be closer to it
    /// </summary>
    private float baseTemperature;
    /// <summary>
    /// determines how the maps center gets calculated
    /// </summary>
    private bool trueCenter;

    /// <summary>
    /// determines at what elevation the water changes to land
    /// </summary>
    private float waterLevel;
    /// <summary>
    /// determines at what elevation the land changes to mountains
    /// </summary>
    private float mountainSize;

    private NoiseMapConfigs elevationMapConfigs;
    private NoiseMapConfigs rainfallMapConfigs;

    //
    // vegetation configs
    //

    //todo

    [JsonProperty]
    public Vector2I WorldSize { get => worldSize; set => worldSize = value; }
    [JsonProperty]
    public int Seed { get => seed; set => seed = value; }
    [JsonProperty]
    public float MapScale { get => mapScale; set => mapScale = value; }
    [JsonProperty]
    public float BaseMoisture { get => baseMoisture; set => baseMoisture = value; }
    [JsonProperty]
    public float BaseTemperature { get => baseTemperature; set => baseTemperature = value; }
    [JsonProperty]
    public float WaterLevel { get => waterLevel; set => waterLevel = value; }
    [JsonProperty]
    public float MountainSize { get => mountainSize; set => mountainSize = value; }
    [JsonIgnore]
    public NoiseMapConfigs ElevationMapConfigs { get => elevationMapConfigs; set => elevationMapConfigs = value; }
    [JsonIgnore]
    public NoiseMapConfigs RainfallMapConfigs { get => rainfallMapConfigs; set => rainfallMapConfigs = value; }
    public bool TrueCenter { get => trueCenter; set => trueCenter = value; }

    public MapConfigs() { DefaultConfigs(); }

    public void DefaultConfigs()
    {
        RandomNumberGenerator rng = new RandomNumberGenerator();

        worldSize = new Vector2I(1000, 1000);

        seed = rng.RandiRange(0, 200);

        mapScale = 1f;
        elevationMapConfigs = new NoiseMapConfigs
        {
            Scale = mapScale,
            Seed = seed,
            Octaves = 3,
            Frequency = 0.01f,
            Lacunarity = 2.5f,
            Gain = 0.4f,
            NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
            FractalType = FastNoiseLite.FractalTypeEnum.Fbm,
            NoiseOffset = Vector2.Zero,
            Normalized = false,
        };

        rainfallMapConfigs = new NoiseMapConfigs
        {
            Scale = mapScale,
            Seed = seed,
            Octaves = 3,
            Frequency = 0.03f,
            Lacunarity = 2.5f,
            Gain = 0.4f,
            NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
            FractalType = FastNoiseLite.FractalTypeEnum.Fbm,
            NoiseOffset = Vector2.Zero,
            Normalized = false,
        };

        waterLevel = -0.23f;
        mountainSize = 0.45f;

        baseMoisture = 0f;
        BaseTemperature = 0.8f;
    }

}