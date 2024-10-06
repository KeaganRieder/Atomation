namespace Atomation.GameMap;

using Atomation.Things;
using Godot;

/// <summary>
/// stores data that has be generated by genSteps and or used  to configure them
/// </summary>
public class GenStepData
{
    public Vector2I Offset { get; set; }
    public Vector2I GenSize { get; set; }

    public WorldSettings GenStepConfigs { get; set; }

    public GenStepNoiseMapsOutput GeneratedNoiseMaps { get; set; }

    /// <summary>
    /// matrix of contains a collection of ground/the landscape for the world generated'
    /// during the landscape generation step
    /// </summary>
    public Terrain[,] GeneratedTerrain { get; set; }

    /// <summary>
    /// matrix of contains a collection of mountains for the world generated'
    /// during the landscape generation step
    /// </summary>
    public Structure[,] GeneratedStructures { get; set; }

    /// <summary>
    /// matrix of contains a collection of plants for the world generated'
    /// during the plant generation step
    /// </summary>
    public Plant[,] GeneratedFoliage { get; set; }

    public GenStepData(Vector2I offset, Vector2I genSize, WorldSettings genStepConfigs)
    {
        Offset = offset;
        GenSize = genSize;
        GenStepConfigs = genStepConfigs;
        
        GeneratedTerrain = new Terrain[genSize.X, genSize.Y];
        GeneratedStructures = new Structure[genSize.X, genSize.Y];
        GeneratedFoliage = new Plant[genSize.X, genSize.Y];
    }
}

/// <summary>
/// a GenStep is used to define a part of the maps generation process
/// </summary>
public abstract class GenStep : Generator<object>
{

    public virtual bool Validate()
    {
        GD.PushWarning("genStep  validation hasn't be implemented");

        return false;
    }
    public virtual void RunStep(GenStepData genStepData)
    {
        GD.PushWarning("genStep hasn't be implemented");
    }
}