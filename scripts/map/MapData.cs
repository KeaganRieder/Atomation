namespace Atomation.Map;

using Godot;
using Newtonsoft.Json;

public class MapData
{
    private static MapData instance;

    /// <summary> how many pixels a tile is 16 x 16 is the normal </summary>
    [JsonIgnore]
    public const int CELL_SIZE = 16;

    [JsonProperty("RenderDistance")]
    private float renderDistance = 32;

    // General info
    [JsonProperty("MapSize")]
    private Vector2 mapSize;
    [JsonProperty("SpawnCords")]
    private Vector2 SpawnCord;
    [JsonProperty("MapSeed")]
    private int Seed = 0;

    //todo make rest of values private
    // Height map
    public float Scale = 1f;
    public int Octaves = 6;
    public float Frequency = 0.02f;
    public float Lacunarity = 2.6f;
    public float Gain = 0.4f;

    [JsonIgnore]
    public FastNoiseLite.NoiseTypeEnum NoiseType = FastNoiseLite.NoiseTypeEnum.SimplexSmooth;
    [JsonIgnore]
    public FastNoiseLite.FractalTypeEnum FractalType = FastNoiseLite.FractalTypeEnum.Fbm;

    //LandScape Settings heights
    public float SeaLevel = -0.2f;
    public float MountainHeight = 0.5f;

    // Moisture map
    public float DewPoint = 20;
    public float PrecipitationIntensity = 0.5f;
    public float FlatteningVal = 0.1f;

    // temperature map
    public float EquatorHeight = 0;
    public float BaseTemperature = 1.0f;
    public float TemperatureMultiplier = 3.05f;
    public float TemperatureLoss = 0.147f;
    public float TemperatureHeightLoss = 0.11f;

    public MapData()
    {
        mapSize = new Vector2(64, 64);
        SpawnCord = new Vector2(0, 0);
    }

    public static MapData GetData()
    {
        if (instance == null)
        {
            instance = new();
        }
        return instance;
    }

    public void Load(MapData Saved)
    {
        instance = Saved;
    }


    /// <summary> sets render distance </summary>
    public void SetRenderDistance(float dist)
    {
        renderDistance = dist;
    }
    /// <summary> sets map size </summary>
    public void SetSize(Vector2 size)
    {
        mapSize = size;
    }
    /// <summary> sets spawn </summary>
    public void SetSpawn(Vector2 cords)
    {
        SpawnCord = cords;
    }
    /// <summary> sets seed </summary>
    public void SetSeed(int val)
    {
        Seed = val;
    }
    /// <summary> randomizes seed </summary>
    public void RandomizeSeed()
    {
        RandomNumberGenerator RNG = new RandomNumberGenerator();
        SetSeed((int)RNG.Randi());
    }

    /// <summary> returns the render distance in terms of tiles </summary>
    public float GetTileRenderDistance()
    {
        return renderDistance * CELL_SIZE;
    }

    /// <summary> returns the render distance in terms of pixels </summary>
    public float GetRenderDistance()
    {
        return renderDistance;
    }

    /// <summary> returns map size </summary>
    public Vector2 GetSize()
    {
        return mapSize;
    }

    /// <summary> returns spawn cords </summary>
    public Vector2 GetSpawn()
    {
        return SpawnCord;
    }

    /// <summary> returns map seed </summary>
    public int GetSeed()
    {
        return Seed;
    }
}