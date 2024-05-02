namespace Atomation.Map;

using Godot;

public class HeightMap : NoiseMap
{
    private FastNoiseLite noiseGen;

    public HeightMap() : base()
    {
        noiseGen = new FastNoiseLite();
        noiseGen.NoiseType = mapData.NoiseType;
        noiseGen.FractalType = mapData.FractalType;
    }

    public override float CalculateNoise(int x, int y)
    {
        float sampleX = (x + offset.X) / mapData.Scale;
        float sampleY = (y + offset.Y) / mapData.Scale;

        noiseGen.Seed = mapData.Seed;
        noiseGen.Frequency = mapData.Frequency;
        noiseGen.FractalGain = mapData.Gain;
        noiseGen.FractalOctaves = mapData.Octaves;

        return noiseGen.GetNoise2D(sampleX,sampleY);
    }
}