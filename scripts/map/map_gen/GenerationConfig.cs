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
/// class stores the config data used during the inital generation of the
/// game world as well as any new chunks
/// </summary>
public class GenerationData
{
    /// <summary>
    /// max world width
    /// </summary>
    public int worldWidth;
    /// <summary>
    /// max world height
    /// </summary>
    public int worldHeight;

    //noise map configs
    //elevation map configs
    public NoiseMapConfig elevationConfig;  
    public float seaLevel; //no sea = 
    public float mountainSize; // no mounatins = 1

    //Moisture map configs  
    public NoiseMapConfig moistureConfig;

    //Heat map configs
    public NoiseMapConfig heatConfig;

    //noise maps
    private float [,] elevationMap;
    private float [,] moistureMap;
    private float [,] heatMap; 
}