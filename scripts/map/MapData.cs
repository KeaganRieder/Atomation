namespace Atomation.Map;

using Godot;
using Newtonsoft.Json;

public class MapData
{
    private static MapData instance;

    /// <summary> how many pixels a tile is 16 x 16 is the normal </summary>
    [JsonIgnore]
    public const int CELL_SIZE = 16;

    public float RenderDistance = 32;

    // General info
    public Vector2 MapSize;
    public Vector2 SpawnCord;

    public int Seed = 0;

    // Height map
    public float Scale = 1;
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
    public float FlatteningVal = 0.44f;

    // temperature map
    public float EquatorHeight = 0;
    public float BaseTemperature = 1.0f;
    public float TemperatureMultiplier = 3.05f;
    public float TemperatureLoss = 0.147f;
    public float TemperatureHeightLoss = 0.11f;

    public MapData()
    {
        MapSize = new Vector2(64, 64);
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

    public void Load(MapData Saved){
        instance = Saved;
    }

    //do validation functions
    public void Validate()
    {
        //todo
    }

    public void RandomizeSeed(){

        RandomNumberGenerator RNG = new RandomNumberGenerator();
        Seed = (int)RNG.Randi();
    }
    

}