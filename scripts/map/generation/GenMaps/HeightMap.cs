namespace Atomation.Map;

using Atomation.Things;
using Godot;
using Newtonsoft.Json;


/// <summary>
/// generates the height/elevation using simplex noise
/// </summary>
public class HeightMap : GenMaps
{
    private float scale;
    private FastNoiseLite noiseGenerator;

    public HeightMap(GenSettings mapGenSettings, int width, int height)
    {
        MapSize = new Vector2I(width, height);
        TotalMapSize = mapGenSettings.WorldSize;
        scale = mapGenSettings.Scale;
        noiseGenerator = new FastNoiseLite();
        UpdateConfigs(mapGenSettings);
    }

    public override void ValidateValues()
    {
        if (scale < 0)
        {
            scale = 0.001f;
        }
        if (noiseGenerator.Seed == -1)
        {
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator();
            noiseGenerator.Seed = numberGenerator.RandiRange(0, 100000);
        }
        if (noiseGenerator.FractalOctaves < 0)
        {
            noiseGenerator.FractalOctaves = 1;
        }

        Mathf.Clamp(noiseGenerator.Frequency, 0, 2);
        Mathf.Clamp(noiseGenerator.FractalGain, 0, 6);
        Mathf.Clamp(noiseGenerator.FractalGain, 0, 6);
        Mathf.Clamp(noiseGenerator.FractalLacunarity, 0, 6);
    }

    public override void UpdateConfigs(GenSettings mapGenSettings)
    {
        TotalMapSize = mapGenSettings.WorldSize;
        scale = mapGenSettings.Scale;
        noiseGenerator.NoiseType = mapGenSettings.HeightMapConfigs.NoiseType;
        noiseGenerator.Seed = mapGenSettings.Seed;
        noiseGenerator.Frequency = mapGenSettings.HeightMapConfigs.Frequency;
        noiseGenerator.FractalType = mapGenSettings.HeightMapConfigs.FractalType;
        noiseGenerator.FractalGain = mapGenSettings.HeightMapConfigs.Gain;
        noiseGenerator.FractalOctaves = mapGenSettings.HeightMapConfigs.Octaves;

        ValidateValues();
    }

    /// <summary>
    /// samples given cords from smaller intervals
    /// </summary>
    private void SampleCords(float x, float y, out float sampleX, out float sampleY)
    {
        sampleX = (x + Offset.X) / scale;
        sampleY = (y + Offset.Y) / scale;
    }

    public void CalculateHeight(float x, float y, Terrain terrain)
    {
        SampleCords(x, y, out float sampleX, out float sampleY);
        float height = noiseGenerator.GetNoise2D(sampleX, sampleY);

        terrain.HeightValue = height;
        terrain.MoistureValue = height + Mathf.Cos(height) * height;

    }
}
