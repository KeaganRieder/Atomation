namespace Atomation.Map;

using Godot;

/// <summary>
/// collection of functions used in converting cords
/// </summary>
public static class CordConversionUtil
{
    /// <summary>
    /// convert to chunk cords from normal cords
    /// </summary>
    public static ChunkCoordinate ToChunkCords(this Coordinate cords)
    {
        return new ChunkCoordinate(cords.GetWorldPosition());
    }
    /// <summary>
    /// convert to chunk cords from vector 2
    /// </summary>
    public static ChunkCoordinate ToChunkCords(this Vector2 cord)
    {
        return new ChunkCoordinate(cord);
    }

    /// <summary>
    /// convert to normal cords from chunk cords
    /// </summary>
    public static Coordinate ToCords(this ChunkCoordinate cords)
    {
        return new Coordinate(cords.GetWorldPosition());
    }

    /// <summary>
    /// converts form a vector 2 to a coordinate
    /// </summary>
    public static Coordinate ToCords(this Vector2 cord){
        return new Coordinate(cord);
    }
}