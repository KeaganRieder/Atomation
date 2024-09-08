namespace Atomation.GameMap;

using Godot;

/// <summary>
/// defines utility used to convert between different cord types
/// </summary>
public static class cordUtil
{
    /// <summary>
    /// takes given global position and converts it to world
    /// this is equal to global /cell size
    /// </summary>
    public static Vector2 GlobalToMap(this Vector2 pos)
    {
        return new Vector2(
            Mathf.FloorToInt(pos.X / Map.CELL_SIZE),
            Mathf.FloorToInt(pos.Y / Map.CELL_SIZE)
        );
    }
    /// <summary>
    /// takes given map position and converts it to world
    /// this is equal to global * cell size
    /// </summary>
    public static Vector2 MapToGlobal(this Vector2 pos)
    {
        return new Vector2(
            pos.X * Map.CELL_SIZE,
            pos.Y * Map.CELL_SIZE
        );
    }
    /// <summary>
    /// converts the map position to the chunk position
    /// </summary>
    public static Vector2 MapToChunk(this Vector2 pos)
    {
        return new Vector2(
            Mathf.FloorToInt(pos.X / Chunk.CHUNK_SIZE),
            Mathf.FloorToInt(pos.Y / Chunk.CHUNK_SIZE)
        );
    }

}