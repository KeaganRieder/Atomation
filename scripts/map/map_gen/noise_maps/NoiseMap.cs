using Godot;

/// <summary>
/// config data used for setting how noise maps are generated
/// </summary>
public class NoiseMapConfig
{
    public int seed;
    public int octaves;
    public float zoom;
    public float frequency;
    public float persistence;
    public float lacunarity;
    /// <summary>
    /// the distance a point is from the center position
    /// </summary>
    public Vector2 offset;
    /// <summary>
    /// the max distance a vertice/point can be form the center point
    /// NOTE: this is mainly used in the generation of the heat map
    /// </summary>
    public Vector2 maxDistance;
    /// <summary>
    /// the center points cords
    /// NOTE: this is mainly used in the generation of the heat map
    /// </summary>
    public Vector2 centerPoint;

}

/// <summary>
/// noise map generates a noise map of various types 
/// based on the a type choosen when it's constructed
/// </summary>
public class NoiseMap : NoiseObject
{
    // The number of noise layers that are sampled to get the final value for fractal noise types.
    private int octaves;
    // Frequency of the noise which warps the space. 
    // Low frequency results in smooth noise while high frequency results in rougher, more granular noise.
    private float frequency;
    //A low value places more emphasis on the lower frequency base layers, 
    //while a high value puts more emphasis on the higher frequency layers
    private float persistence;
    // Frequency multiplier between subsequent octaves.
    // Increasing this value results in higher octaves producing noise with finer details and a rougher appearance.
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
    /// <summary>
    /// creates a NoiseMap object which is set to generate simplex noise
    /// </summary>
    public NoiseMap(NoiseMapConfig config){
        
        noiseLite = new FastNoiseLite(){
            NoiseType = FastNoiseLite.NoiseTypeEnum.SimplexSmooth
        };
        Seed = config.seed;
        ZoomLevel = config.zoom; 
        Octaves = config.octaves;
        Frequency = config.frequency;
        Persistence = config.persistence;
        Lacunarity = config.lacunarity;  
        // noiseLite.peri      
    }

    public void UpdateConfigs(NoiseMapConfig config){
        Seed = config.seed;
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
            //ensure range of noise is between values -1 and 1
            return noiseLite.GetNoise2D(x,y);
        }
    }
    public override float this[Vector2 cords]{
        get{
            //ensure range of noise is between values -1 and 1
            return noiseLite.GetNoise2Dv(cords);
        }
    } 
}
