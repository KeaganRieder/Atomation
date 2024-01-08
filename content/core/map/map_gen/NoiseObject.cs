using System;
using System.Diagnostics;
using Godot;

/// <summary>
/// handles the storing of varible which used in generating a matirx of float value swhich range 
/// in value from -1 to 1
/// </summary>
public class NoiseObject
{
    private int seed;
    private int seedOffset;
    private int octaves;
    private float zoom;
    private float frequency;
    private float persistence;
    private float lacunarity;

    private FastNoiseLite noiseMap;

    public NoiseObject(){
        noiseMap = new FastNoiseLite(){
            NoiseType = FastNoiseLite.NoiseTypeEnum.SimplexSmooth
        };
    }

    public FastNoiseLite NoiseMap{get => noiseMap;}
    public int Seed{
        get =>seed;
        set{
            seed = value;
            if (noiseMap != null)
            {
                noiseMap.Seed = value + seedOffset;
            }
        }
    }
    public int SeedOffset{
        get =>seedOffset;
        set{
            seedOffset = value;
        }
    }
    public int Octaves{
        get =>octaves;
        set{
            octaves = Mathf.Max(1,value) ;
            if (noiseMap != null)
            {
                noiseMap.FractalOctaves = octaves;
            }
        }
    }
    public float Zoom{
        get => zoom;
        set{
            zoom = Math.Max(1, value);
        }
    }
    public float Frequency{
        get => frequency;
        set{
            frequency = value;
            if (noiseMap != null)
            {
                noiseMap.Frequency = value/zoom;
            }
        }
    }
    public float Persistence{
        get => persistence;
        set{
            persistence = value;
            if (noiseMap != null)
            {
                noiseMap.FractalGain = value;
            }
        }
    }
    public float Lacunarity{
        get => lacunarity;
        set{
            lacunarity = value;
            if (noiseMap != null)
            {
                noiseMap.FractalLacunarity = value;
            }
        }
    }

    public float this[int x, int y]{
        get{
            return noiseMap.GetNoise2D(x,y);
        }
    }
    public float this[Vector2 cords]{
        get{
            return noiseMap.GetNoise2Dv(cords);
        }
    }   
}