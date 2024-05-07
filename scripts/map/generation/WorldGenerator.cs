namespace Atomation.Map;

using Godot;

/// <summary>
/// the games world generators, which manage and oversees
/// the running/execution of gensteps to generate the game
/// world or chunks with in it
/// </summary>
public class WorldGenerator
{
    private static WorldGenerator instance;

    private GenStepLandscape genStepLandscape;

    public WorldGenerator()
    {
        genStepLandscape = new GenStepLandscape();
    }

    public static WorldGenerator GetInstance()
    {
        if (instance == null)
        {
            instance = new WorldGenerator();
        }
        return instance;
    }

    public void RegenerateMap(){
        WorldMap map = WorldMap.GetInstance();
    }

    /// <summary> 
    /// runs gen steps to generate the initial chunks in a map
    /// these are general the 'spawn chunks' 
    /// </summary>
    public void GenerateMap()
    {
        GD.Print("Generating map");
        MapData mapData = MapData.GetData();
        WorldMap map = WorldMap.GetInstance();
        Coordinate spawn = new Coordinate(mapData.GetSpawn());

        map.ClearMap();
        map.UpdateVisibleChunks(spawn);
        GD.Print("Generation complete");
    }

    /// <summary> runs gen steps to generate a new chunk </summary>
    public void GenerateChunk(Vector2 origin)
    {
        genStepLandscape.SetGenSize(new Vector2(Chunk.CHUNK_SIZE, Chunk.CHUNK_SIZE));
        genStepLandscape.SetOrigin(origin);

        genStepLandscape.RunStep();
    }




}