namespace Atomation.Map;

using Godot;

/// <summary>
/// the games world generators, which manage and oversees
/// the running/execution of gensteps to generate the game
/// world or chunks with in it
/// </summary>
public static class WorldGenerator
{
    private static GenStepLandScape genStepLandScape;

    public static GenSettings GenConfig { get; set; }

    public static void Initialize(GenSettings configs)
    {
        GenConfig = configs;
        genStepLandScape = new GenStepLandScape(GenConfig);
    }

    /// <summary>
    /// Used to Generate new Chunks
    /// </summary>
    public static void GenerateChunk(Vector2 ChunkCord, WorldMap chunkHandler)
    {
        genStepLandScape.RunStep(ChunkCord, chunkHandler);
    }
}