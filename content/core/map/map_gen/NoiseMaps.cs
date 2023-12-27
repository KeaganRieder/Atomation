using System;
using Godot;

/// <summary>
/// NoiseMap is the base noiseMap generation function
/// 
/// </summary>
public class NoiseMap
{  
    protected FastNoiseLite fastNoiseLite;
    
    public NoiseMap(){

        fastNoiseLite.Frequency = 0;
        fastNoiseLite.FractalOctaves =0;
        fastNoiseLite.FractalGain = 0;
        fastNoiseLite.FractalWeightedStrength = 0;
    }
    public NoiseMap(int seed, int octaves, float frequency,float lacunarity, float fractalGain){
        fastNoiseLite = new FastNoiseLite();
        fastNoiseLite.FractalType = FastNoiseLite.FractalTypeEnum.Fbm;
        fastNoiseLite.Seed = seed;
        fastNoiseLite.FractalLacunarity = lacunarity;
        fastNoiseLite.Frequency = frequency;
        fastNoiseLite.FractalOctaves = octaves;
        fastNoiseLite.FractalGain = fractalGain;
    }
    
    public float GetNoise(int x, int y){
        return fastNoiseLite.GetNoise2D(x,y);
    }
    public float GetNoise(Vector2 cords){
        return fastNoiseLite.GetNoise2Dv(cords);
    }

   
}


/// <summary>
/// SimplexNoise generates a simplex nosie map
/// </summary>
public class SimplexNoise: NoiseMap
{
    public SimplexNoise(int seed, int octaves, float frequency,float lacunarity, float fractalGain) :
            base(seed,octaves,frequency,lacunarity,fractalGain){
        fastNoiseLite.NoiseType = FastNoiseLite.NoiseTypeEnum.SimplexSmooth;
    }

}

/// <summary>
/// PerlinNoise generates a perlin nosie map
/// </summary>
public class PerlinNoise : NoiseMap
{
    public PerlinNoise(int seed, int octaves, float frequency,float lacunarity, float fractalGain) :
            base(seed,octaves,frequency,lacunarity,fractalGain){
        fastNoiseLite.NoiseType = FastNoiseLite.NoiseTypeEnum.SimplexSmooth;
    }
  
}


