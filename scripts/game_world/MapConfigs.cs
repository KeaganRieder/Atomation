// namespace Atomation.GameMap;

// using System.Collections.Generic;
// using Godot;

// /// <summary>
// /// Contains the configs for the world. these configs are used to decide anything from
// /// the size of the world to how the generators generate it
// /// </summary>
// public class MapConfigs
// {
//     /// <summary>
//     /// the size of the game world
//     /// </summary>
//     public Vector2I WorldSize { get; set; }

//     public int MapSeed { get; set; }
//     /// <summary>
//     /// how zoomed in or out the map is
//     /// </summary>
//     public float MapZoom { get; set; }

//     //
//     // world elevation
//     //
//     /// <summary>
//     /// determines at what elevation the water changes to land
//     /// </summary>
//     public float WaterLevel { get; set; }
//     /// <summary>
//     /// determines at what elevation the land changes to mountains
//     /// </summary>
//     public float MountainSize { get; set; }

//     // public FastNoiseLite ElevationNoise{get; set}
//     public NoiseMapConfigs ElevationMapConfigs { get; set; }

//     //
//     // world temperature
//     //

//     /// <summary>
//     /// worlds base temp
//     /// </summary>
//     public float BaseTemperature { get; set; }
//     /// <summary>
//     /// determines if temperature is based on the center point (0,0)
//     /// of if it's teh center of the world size
//     /// </summary>
//     public bool TrueCenter { get; set; }

//     //
//     // world moisture
//     //   
//     /// <summary>
//     /// base moisture of teh world
//     /// </summary>
//     public float BaseMoisture { get; set; }

//     public NoiseMapConfigs RainfallMapConfigs { get; set; }

//     //
//     // vegetation moisture
//     //  
//     public NoiseMapConfigs TreeDensityMapConfigs { get; set; }

//     //
//     // resources
//     //
//     //todo

//     public MapConfigs()
//     {
//         DefaultConfigs();
//     }


//     /// <summary>
//     /// sets the configs for the world to be default
//     /// </summary>
//     public void DefaultConfigs()
//     {
//         RandomNumberGenerator rng = new RandomNumberGenerator();

//         WorldSize = new Vector2I(1000, 1000);

//         MapSeed = rng.RandiRange(0, 200);

//         MapZoom = 1f;

//         WaterLevel = -0.23f;
//         MountainSize = 0.45f;
//  ElevationMapConfigs = new NoiseMapConfigs
//         {
//             Scale = MapZoom,
//             Seed = MapSeed,
//             Octaves = 3,
//             Frequency = 0.01f,
//             Lacunarity = 2.5f,
//             Gain = 0.4f,
//             NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
//             FractalType = FastNoiseLite.FractalTypeEnum.Fbm,
//             NoiseOffset = Vector2.Zero,
//             Normalized = false,
//         };

//         BaseMoisture = 0f;
//         RainfallMapConfigs = new NoiseMapConfigs
//         {
//             Scale = MapZoom,
//             Seed = MapSeed,
//             Octaves = 3,
//             Frequency = 0.03f,
//             Lacunarity = 2.5f,
//             Gain = 0.4f,
//             NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
//             FractalType = FastNoiseLite.FractalTypeEnum.Fbm,
//             NoiseOffset = Vector2.Zero,
//             Normalized = false,
//         };

//         TreeDensityMapConfigs = new NoiseMapConfigs
//         {
//             Scale = MapZoom,
//             Seed = MapSeed,
//             Octaves = 3,
//             Frequency = 0.05f,
//             Lacunarity = 2.5f,
//             Gain = 0.4f,
//             NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
//             FractalType = FastNoiseLite.FractalTypeEnum.Fbm,
//             NoiseOffset = Vector2.Zero,
//             Normalized = false,
//         };
//         BaseTemperature = 0.8f;
//     }

//     public void SaveConfigs()
//     {
//         GD.PushWarning("saving of map configs not implemented");
//     }
//     public void LoadConfigs()
//     {
//         GD.PushWarning("loading of map configs not implemented");
//     }
// } 