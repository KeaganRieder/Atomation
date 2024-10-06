namespace Atomation.GameMap;

using System.Collections.Generic;
using Godot;

/// <summary>
/// stores the settings for the world. these are used to configure
/// the different world generators among other aspects of the world
/// </summary>
public class WorldSettings
{
    private RandomNumberGenerator rng;
    
    /// <summary>
    /// determines if temperature is based on the center point (0,0)
    /// of if it's teh center of the world size
    /// </summary>
    public bool TrueCenter { get; set; }

    public Vector2I WorldSize { get; set; }

    public int MapSeed { get; set; }

    /// <summary>
    /// how zoomed in or out the map is
    /// </summary>
    public float MapZoom { get; set; }

    /// <summary>
    /// determines at what elevation the water changes to land
    /// </summary>
    public float WaterLevel { get; set; }
    /// <summary>
    /// determines at what elevation the land changes to mountains
    /// </summary>
    public float MountainSize { get; set; }


    /// <summary>
    /// worlds base moisture
    /// </summary>
    public float BaseMoisture { get; set; }
    /// <summary>
    /// how much the base moisture effects the final moisture value
    /// </summary>
    public float MoistureStrength { get; set; }

    /// <summary>
    /// worlds base temperature
    /// </summary>
    public float BaseTemperature { get; set; }
    /// <summary>
    /// how much the base temperature effects the final moisture value
    /// </summary>
    public float TemperatureStrength { get; set; }

    public float TreeDensity{get;set;}

    public FastNoiseLite ElevationMap { get; set; }
    public FastNoiseLite TemperatureMap { get; set; }
    public FastNoiseLite MoistureMap { get; set; }

    public FastNoiseLite TreeDensityMap { get; set; }

    public WorldSettings()
    {
        rng = new RandomNumberGenerator();
        ElevationMap = new FastNoiseLite();
        MoistureMap = new FastNoiseLite();
        TemperatureMap = new FastNoiseLite();
        TreeDensityMap = new FastNoiseLite();

        DefaultSettings();
    }

    public void DefaultSettings()
    {
        TrueCenter = false;

        WorldSize = new Vector2I(1000, 1000);

        MapSeed =  rng.RandiRange(0, 1000);
        MapZoom = .5f;

        WaterLevel = -0.1f;
        MountainSize = 0.45f;

        BaseTemperature = .8f;
        TemperatureStrength = 0;
        BaseMoisture = .8f;
        MoistureStrength = 0;

        TreeDensity = .8f; // between 0 and 1 (0% and 100%)

        //todo make offset for seeds
        ElevationMap.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
        ElevationMap.FractalType = FastNoiseLite.FractalTypeEnum.Fbm;
        ElevationMap.FractalOctaves = 5;
        ElevationMap.Frequency = 0.01f;
        ElevationMap.FractalLacunarity = 2.0f;
        ElevationMap.FractalGain = 0.4f;
        ElevationMap.Seed = MapSeed;

        MoistureMap.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
        MoistureMap.FractalType = FastNoiseLite.FractalTypeEnum.Fbm;
        MoistureMap.FractalOctaves = 5;
        MoistureMap.Frequency = 0.01f;
        MoistureMap.FractalLacunarity = 2;
        MoistureMap.FractalGain = 0.4f;
        MoistureMap.Offset = new Vector3(100, 100, 0);
        ElevationMap.Seed = MapSeed;

        TemperatureMap.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
        TemperatureMap.FractalType = FastNoiseLite.FractalTypeEnum.Fbm;
        TemperatureMap.FractalOctaves = 5;
        TemperatureMap.Frequency = 0.01f;
        TemperatureMap.FractalLacunarity = 2;
        TemperatureMap.FractalGain = 0.4f;
        TemperatureMap.Offset = new Vector3(200, 200, 0);
        ElevationMap.Seed = MapSeed;

        TreeDensityMap.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
        TreeDensityMap.FractalType = FastNoiseLite.FractalTypeEnum.Fbm;
        TreeDensityMap.FractalOctaves = 5;
        TreeDensityMap.Frequency = 0.01f;
        TreeDensityMap.FractalLacunarity = 2f;
        TreeDensityMap.FractalGain = 0.4f;
        TreeDensityMap.Offset = new Vector3(50, 50, 0);
        ElevationMap.Seed = MapSeed;
    }

    public void Save(){

    }
    public void Load(){
        
    }
}