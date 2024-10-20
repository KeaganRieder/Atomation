namespace Atomation.GameMap;

using Atomation;
using Godot;
using System.Collections.Generic;

/// <summary>
/// A collection of values used to configure the world generation
/// </summary>
public class WorldConfigs
{
    public Dictionary<string, object> Values { get; set; }

    public Dictionary<string, SimplexNoiseConfigs> SimplexNoiseMapConfigs { get; set; }

    public WorldConfigs()
    {
        Values = new Dictionary<string, object>();
        SimplexNoiseMapConfigs = new Dictionary<string, SimplexNoiseConfigs>();

        DefaultSettings();
    }

    /// <summary>
    /// how zoomed in are the noise maps
    /// </summary>
    public float ZoomLevel { get; set; } = 1;
    public Vector2I WorldSize
    {
        get
        {
            int width = TypeConverter.ToInt((string)Values["General_Width"]);
            int height = TypeConverter.ToInt((string)Values["General_Height"]);
            return new Vector2I(width, height);
        }
    }

    public float GetWaterLevel()
    {
        float waterLevel = (float)Values["Elevation_WaterLevel"];

        if (waterLevel == 0)
        {
            return -1;
        }
        
        return (float)Values["Elevation_WaterLevel"] * 2 - 1;
    }
    public float GetMountainSize()
    {
        float mountainSize = (float)Values["Elevation_MountainSize"];

        if (mountainSize == 0)
        {
            return -1;
        }
        // else if (mountainSize == 1)
        // {
        //     return mountainSize;            
        // }

        return (float)Values["Elevation_WaterLevel"] * 2 - 1;
    }

    /// <summary>
    /// configures noise maps with current settings
    /// </summary>
    public void ConfigureNoiseMaps()
    {
        int seed = TypeConverter.ToInt((string)Values["General_Seed"]);
        foreach (var noiseMap in SimplexNoiseMapConfigs)
        {
            noiseMap.Value.Noise.Seed = seed;
        }
    }

    /// <summary>
    /// sets the world to default settings preset
    /// </summary>
    public void DefaultSettings()
    {
        SimplexNoiseMapConfigs["Elevation"] = new SimplexNoiseConfigs
        {
            NoiseOffset = new Vector2I(0, 0),
            Noise = new FastNoiseLite
            {
                Frequency = 0.05f,
                FractalLacunarity = 2.5f,
                FractalOctaves = 5,
                FractalGain = 0.5f,
                FractalWeightedStrength = 0,
            },

        };
        // SimplexNoiseMapConfigs["Elevation"] = new SimplexNoiseConfigs
        // {
        //     NoiseOffset = new Vector2I(100, 100),
        //     Noise = new FastNoiseLite
        //     {
        //         Frequency = 0.05f,
        //         FractalLacunarity = 2.5f,
        //         FractalOctaves = 5,
        //         FractalGain = 0.5f,
        //         FractalWeightedStrength = 0,
        //     },
        // };
        SimplexNoiseMapConfigs["Moisture"] = new SimplexNoiseConfigs
        {
            NoiseOffset = new Vector2I(100, 100),
            Noise = new FastNoiseLite
            {
                Frequency = 0.05f,
                FractalLacunarity = 2.5f,
                FractalOctaves = 5,
                FractalGain = 0.5f,
                FractalWeightedStrength = 0,
            },
        };
        SimplexNoiseMapConfigs["Temperature"] = new SimplexNoiseConfigs
        {
            NoiseOffset = new Vector2I(200, 200),
            Noise = new FastNoiseLite
            {
                Frequency = 0.05f,
                FractalLacunarity = 2.5f,
                FractalOctaves = 5,
                FractalGain = 0.5f,
                FractalWeightedStrength = 0,
            },
        };
        SimplexNoiseMapConfigs["TreeMap"] = new SimplexNoiseConfigs
        {
            NoiseOffset = new Vector2I(200, 200),
            Noise = new FastNoiseLite
            {
                Frequency = 0.05f,
                FractalLacunarity = 2.5f,
                FractalOctaves = 5,
                FractalGain = 0.5f,
                FractalWeightedStrength = 0,
            },
        };
    }
}

/// <summary>
/// A collection of values used to configure the simplex noise map generators
/// </summary>
public class SimplexNoiseConfigs
{
    /// <summary>
    /// used to determine how much of an offset is the noise gotten from 
    /// </summary>
    public Vector2I NoiseOffset { get; set; }
    /// <summary>
    /// the noise object used to get values from
    /// </summary>
    public FastNoiseLite Noise { get; set; }

    public SimplexNoiseConfigs()
    {
        Noise = new FastNoiseLite
        {
            NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex,
            FractalType = FastNoiseLite.FractalTypeEnum.Fbm
        };
    }

    public float GetNoise(float x, float y)
    {
        return Noise.GetNoise2D(x, y);
    }

    /// <summary>
    /// gets noise from simplex noise configs which is normalized to be between 0 and 1 form -1 to 1
    /// </summary>
    public float GetNormalizedNoise(float x, float y)
    {
        return (Noise.GetNoise2D(x, y) + 1) / 2;

    }
}
