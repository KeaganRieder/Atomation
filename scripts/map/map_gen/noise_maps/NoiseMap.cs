using Godot;

/// <summary>
/// noise map generates a noise map of various types 
/// based on the a type choosen when it's constructed
/// </summary>
public class NoiseMap : NoiseObject
{
    private int octaves;
    private float frequency;
    private float persistence;
    private float lacunarity;
    private FastNoiseLite noiseLite;

    public NoiseMap(){
        noiseLite = new FastNoiseLite(){
            NoiseType = FastNoiseLite.NoiseTypeEnum.SimplexSmooth
        };    
    }
    /// <summary>
    /// creates a NoiseMap object which is set to generate simplex noise
    /// </summary>
    public NoiseMap(int seed, float zoomLevel, int octaves, 
        float frequency, float persistence, float lacunarity){
        
        noiseLite = new FastNoiseLite(){
            NoiseType = FastNoiseLite.NoiseTypeEnum.SimplexSmooth
        };
        Seed = seed;
        ZoomLevel = zoomLevel; //maybe make zoom level change able later
        Octaves = octaves;
        Frequency = frequency;
        Persistence = persistence;
        Lacunarity = lacunarity;        
    }
    public NoiseMap(NoiseMapConfig config){
        
        noiseLite = new FastNoiseLite(){
            NoiseType = FastNoiseLite.NoiseTypeEnum.SimplexSmooth
        };
        Seed = seed;
        ZoomLevel = config.zoom; 
        Octaves = config.octaves;
        Frequency = config.frequency;
        Persistence = config.persistence;
        Lacunarity = config.lacunarity;        
    }

    public override int Seed{
        get => seed; 
        set{
            seed = value;
            if (noiseLite != null)
            {
                noiseLite.Seed = value;
            }
        }
    }
    public override Vector2 Offset{
        get => mapOffset; 
        set{
            mapOffset = value;
            if (noiseLite != null)
            {
                noiseLite.Offset = new Vector3(mapOffset.X,mapOffset.Y, 0);
            }
        }
    }
    
    public int Octaves {
        get => octaves; 
        set{
            octaves = Mathf.Max(1,value);
            if (noiseLite != null)
            {
                noiseLite.FractalOctaves = octaves;
            }
        }
    }
    public float Frequency {
        get=>frequency; 
        set{
            frequency = value;
            if (noiseLite != null)
            {
                noiseLite.Frequency = frequency/zoomLevel;
            }
        }
    }
    public float Persistence {
        get=>persistence; 
        set{
            persistence = value;
            if (noiseLite != null)
            {
                noiseLite.FractalGain = persistence;
            }
        }
    }
    public float Lacunarity {
        get=>lacunarity; 
    set{
            lacunarity = value;
            if (noiseLite != null)
            {
                noiseLite.FractalLacunarity = lacunarity;
            }
        }
    }

    public override float this[int x, int y]{
        get{
            return noiseLite.GetNoise2D(x,y);
        }
    }
    public override float this[Vector2 cords]{
        get{
            return noiseLite.GetNoise2Dv(cords);
        }
    } 
}
