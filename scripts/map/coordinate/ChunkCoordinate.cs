namespace Atomation.Map;

using Newtonsoft.Json;
using Godot;

/// <summary>
/// a Chunks Coordinate in the game world contains, methods convert
/// it between different grids
/// </summary>
public class ChunkCoordinate : Coordinate
{
    [JsonConstructor]
    protected ChunkCoordinate() : base() { }

    public ChunkCoordinate(Vector2 position)
    {
        SetPosition(position);
    }
    public ChunkCoordinate(int x, int y)
    {
        SetPosition(x, y);
    }

    /// <summary>
    /// converts X Y position to the world position  
    /// </summary>
    protected override void WorldFromXY()
    {
        worldPosition = XYPosition * Chunk.TOTAL_CHUNK_SIZE;
    }

    /// <summary>
    /// converts world position to the X,Y position
    /// </summary>
    protected override void XYFromWorld()
    {
        int x = Mathf.FloorToInt(worldPosition.X / Chunk.TOTAL_CHUNK_SIZE);
        int y = Mathf.FloorToInt(worldPosition.Y / Chunk.TOTAL_CHUNK_SIZE);

        XYPosition = new Vector2I(x, y);
    }

    /// <summary>
    /// sets the x y position
    /// </summary>
    protected override void SetPosition(int x, int y)
    {
        XYPosition = new Vector2I(x, y);
        WorldFromXY();
    }

    /// <summary>
    /// sets the world position
    /// </summary>
    public override void SetPosition(Vector2 position)
    {
        worldPosition = FindChunkWorldPos(position);
        XYFromWorld();
    }
}